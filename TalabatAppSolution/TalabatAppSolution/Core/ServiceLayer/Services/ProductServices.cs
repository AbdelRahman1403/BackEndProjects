using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models.ProductModels;
using DomainLayer.UOW;
using ServiceAbstractionLayer.IServices;
using ServiceLayer.Spceifications;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductServices(IUnitOfWork unitOfWork , IMapper mapper) : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var spec = new ProductSpecification();
            var products = await _unitOfWork.GetRepo<Products , int>().GetAllWithSpecificationsAsync(spec);

            return mapper.Map<IEnumerable<Products>, IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.GetRepo<Products, int>().GetByIdSpecificationsAsync(spec);

            return mapper.Map<Products, ProductDto>(product);
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepo<ProductBrand, int>().GetAllAsync();

            return mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(brands);
        }
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepo<ProductType, int>().GetAllAsync();

            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }
    }
}
