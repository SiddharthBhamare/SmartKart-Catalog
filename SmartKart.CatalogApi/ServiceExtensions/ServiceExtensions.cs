using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

using System.Text.Json;

namespace SmartKart.CatalogApi.Infrastructure
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds and configures Swagger, JWT authentication, and authorization policies.
        /// This method encapsulates all the related services for a cleaner Program.cs file.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <param name="configuration">The IConfiguration instance to read settings from.</param>
        public static void AddSwaggerAndAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Register and configure the AuthenticationOptions class from the configuration
            services.Configure<OptionClasses.AuthenticationOptions>(configuration.GetSection(OptionClasses.AuthenticationOptions.SectionName));
            var authOptions = configuration.GetSection(OptionClasses.AuthenticationOptions.SectionName).Get<OptionClasses.AuthenticationOptions>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Product Service",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Use the configured options to set the authority, audience, and issuer
                    options.Authority = authOptions.Authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                    };
                    options.RequireHttpsMetadata = false;

                    // This is for local development with self-signed certificates
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };

                    // Add a diagnostic event to log the specific failure reason
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        // This event handler extracts roles from Keycloak's non-standard claim
                        OnTokenValidated = context =>
                        {
                            var realmAccessClaim = context.Principal.FindFirst("realm_access")?.Value;
                            if (!string.IsNullOrEmpty(realmAccessClaim))
                            {
                                var realmAccess = JsonDocument.Parse(realmAccessClaim).RootElement;
                                var roles = realmAccess.GetProperty("roles").EnumerateArray();
                                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                                if (claimsIdentity != null)
                                {
                                    foreach (var role in roles)
                                    {
                                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                                    }
                                }
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
                options.AddPolicy("VendorOnly", policy => policy.RequireRole("vendor"));
                options.AddPolicy("AdminOrVendor", policy => policy.RequireRole("admin", "vendor"));
            });
        }

        /// <summary>
        /// Adds and configures the Swagger and authentication middleware.
        /// </summary>
        /// <param name="app">The WebApplication instance.</param>
        public static void UseSwaggerAndAuthentication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
