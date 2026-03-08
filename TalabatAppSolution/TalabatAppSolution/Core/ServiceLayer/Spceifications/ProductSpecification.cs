using DomainLayer.Models.ProductModels;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Spceifications
{
    public class ProductSpecification : BasicSpecification<Products , int>
    {
        public ProductSpecification(ProductQueryPrams productQuery) :
        base(ch => (!productQuery.BrandId.HasValue || ch.BrandId == productQuery.BrandId.Value) &&
                   (!productQuery.TypeId.HasValue  || ch.TypeId == productQuery.TypeId.Value) &&
                   (string.IsNullOrEmpty(productQuery.SearchValue) || ch.Name.ToLower().Contains(productQuery.SearchValue.ToLower()))
            )
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

            switch (productQuery.sortingOptions)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }

            ApplyPaginaging(productQuery.PageIndex, productQuery.PageSize);
        }
        public ProductSpecification(int id) : base(ch => ch.Id == id)
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }

    }
}
