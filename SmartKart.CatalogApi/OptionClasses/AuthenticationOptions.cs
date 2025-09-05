namespace SmartKart.CatalogApi.OptionClasses
{
    public class AuthenticationOptions
    {
        public const string SectionName = "Authentication";
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
