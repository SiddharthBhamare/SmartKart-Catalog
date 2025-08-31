using SmartKart.CatalogApi.Application.Dto;

namespace SmartKart.CatalogApi.Application.Interfaces
{
    public interface IPaginationService
    {
        /// <summary>
        /// Paginates a queryable dataset into a standard paged result.
        /// </summary>
        /// <typeparam name="T">The type of data (Product, Category, etc.)</typeparam>
        /// <param name="query">The IQueryable source</param>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <returns>Paged result containing data and metadata</returns>
        Task<PagedResultDto<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize);
    }
}
