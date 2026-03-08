using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Exceptions.ProductExceptions;
using DomainLayer.Models.ProductModels;
using DomainLayer.UOW;
using ServiceAbstractionLayer.IServices;
using ServiceLayer.Spceifications;
using Shared.Common;
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

        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryPrams productQuery)
        {
            var spec = new ProductSpecification(productQuery);
            var products = await _unitOfWork.GetRepo<Products , int>().GetAllWithSpecificationsAsync(spec);

            var Data =  mapper.Map<IEnumerable<Products>, IEnumerable<ProductDto>>(products);
            var Size = Data.Count();

            var CountSpec = new CountProductSpecification(productQuery);
            var TotalCount = await _unitOfWork.GetRepo<Products, int>().GetCountWithSpecificationsAsync(CountSpec);
            return new PaginationResult<ProductDto>(productQuery.PageIndex,Size,TotalCount,Data);
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.GetRepo<Products, int>().GetByIdSpecificationsAsync(spec);
            if (product is null)
                throw new ProductException(id);

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
