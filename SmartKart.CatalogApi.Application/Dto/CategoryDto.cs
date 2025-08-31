﻿namespace SmartKart.CatalogApi.Application.Dto
{
    public sealed class CategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
    }
}
