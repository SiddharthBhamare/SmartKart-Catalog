using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Application.Exceptions;
using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Application.Mappers;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Exceptions;

namespace SmartKart.CatalogApi.Application.Services
{
    public sealed class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            return brand is null ? null : BrandMapper.ToDto(brand);
        }

        public async Task<IReadOnlyList<BrandDto>> GetAllAsync(CancellationToken ct = default)
        {
            var items = await _unitOfWork.Brands.ListAsync();
            return items.Select(BrandMapper.ToDto).ToList();
        }

        public async Task<BrandDto> CreateBrandAsync(BrandDto brandDto)
        {
            if (string.IsNullOrWhiteSpace(brandDto.Name))
            {
                throw new DomainException("Brand name cannot be null or empty.");
            }
            var brand = new Brand(brandDto.Name);
            brand.Description = brandDto.Description;
            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.SaveChangesAsync();
            return BrandMapper.ToDto(brand);
        }

        public async Task<List<BrandDto>> CreateBrandsAsync(List<BrandDto> brands)
        {
            var createdBrands = new List<Brand>();
            foreach (var brandDto in brands)
            {
                if (string.IsNullOrWhiteSpace(brandDto.Name))
                {
                    continue; // Skip empty names
                }
                var brand = new Brand(brandDto.Name);
                brand.Description = brandDto.Description;
                await _unitOfWork.Brands.AddAsync(brand);
                createdBrands.Add(brand);
            }
            await _unitOfWork.SaveChangesAsync();
            return createdBrands.Select(BrandMapper.ToDto).ToList();
        }

        public async Task<BrandDto> GetBrandAsync(Guid id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand == null)
            {
                throw new BrandNotFoundException(id);
            }
            return BrandMapper.ToDto(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                var allBrands = await _unitOfWork.Brands.ListAsync();
                return allBrands.Select(BrandMapper.ToDto);
            }

            var guids = ids.Split(',')
                .Select(id => Guid.TryParse(id.Trim(), out var guid) ? (Guid?)guid : null)
                .Where(guid => guid.HasValue)
                .Select(guid => guid.Value)
                .ToList();

            var brands = await _unitOfWork.Brands.ListAsync();
            var filteredBrands = brands.Where(b => guids.Contains(b.Id)).ToList();

            return filteredBrands.Select(BrandMapper.ToDto);
        }

        public async Task UpdateBrandAsync(Guid id, string newName)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand == null)
            {
                throw new BrandNotFoundException(id);
            }

            brand.Rename(newName);
            await _unitOfWork.Brands.UpdateAsync(brand);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand == null)
            {
                throw new BrandNotFoundException(id);
            }

            await _unitOfWork.Brands.DeleteAsync(brand);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
