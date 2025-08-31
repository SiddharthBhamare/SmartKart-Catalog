using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace SmartKart.CatalogApi.Application.Interfaces
{
    public interface IBrandService
    {
        Task<BrandDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<BrandDto>> GetAllAsync(CancellationToken ct = default);
        /// <summary>
        /// Creates a single new brand.
        /// </summary>
        /// <param name="brandName">The name of the brand to create.</param>
        Task<BrandDto> CreateBrandAsync(BrandDto brandDto);

        /// <summary>
        /// Creates multiple new brands in a batch.
        /// </summary>
        /// <param name="brands">A list of BrandDto objects containing the names and descriptions.</param>
        Task<List<BrandDto>> CreateBrandsAsync(List<BrandDto> brands);

        /// <summary>
        /// Gets a brand by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve.</param>
        Task<BrandDto> GetBrandAsync(Guid id);

        /// <summary>
        /// Gets multiple brands by their unique IDs.
        /// </summary>
        /// <param name="ids">A comma-separated string of brand IDs.</param>
        Task<IEnumerable<BrandDto>> GetBrandsAsync(string ids);

        /// <summary>
        /// Renames an existing brand.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="newName">The new name for the brand.</param>
        Task UpdateBrandAsync(Guid id, string newName);

        /// <summary>
        /// Deletes a brand.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        Task DeleteBrandAsync(Guid id);
    }
}