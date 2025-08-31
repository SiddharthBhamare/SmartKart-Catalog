using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Interfaces;

namespace SmartKart.CatalogApi.Infrastructure.Services
{
    public class PaginationService : IPaginationService
    {
        public async Task<PagedResultDto<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
