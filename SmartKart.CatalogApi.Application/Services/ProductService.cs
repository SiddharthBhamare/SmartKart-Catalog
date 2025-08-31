using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Exceptions;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService _paginationService;

        public ProductService(IUnitOfWork unitOfWork, 
            IPaginationService paginationService
            )
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Products.AsQueryable();
            var pagedResult = await _paginationService.PaginateAsync(query, pageNumber, pageSize);
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (var item in pagedResult.Items)
            {
                productDtos.Add(Mappers.ProductMapper.ToDto(item));
            }
            return productDtos as IReadOnlyList<ProductDto>;
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return Mappers.ProductMapper.ToDto(product);
        }

        public async Task AddProductAsync(ProductDto product)
        {
            await _unitOfWork.Products.AddAsync(Mappers.ProductMapper.FromCreate(product));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task AddProductsAsync(List<ProductDto> products)
        {
            foreach (var product in products)
            {
                await _unitOfWork.Products.AddAsync(Mappers.ProductMapper.FromCreate(product));
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(ProductDto product)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new ProductNotFoundException(product.Id);
            }
            existingProduct.Update(product.Name, existingProduct.SKU, existingProduct.Price, existingProduct.Description, existingProduct.CategoryId, existingProduct.BrandId, existingProduct.StockQuantity);

            await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId)
        {
           return await _unitOfWork.Products.GetProductsByCategoryIdAsync(categoryId) as IReadOnlyList<ProductDto>;
        }
    }
}
