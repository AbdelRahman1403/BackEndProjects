using DomainLayer.Models.ProductModels;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Spceifications
{
    public class CountProductSpecification : BasicSpecification<Products , int>
    {
        public CountProductSpecification(ProductQueryPrams productQuery) :
        base(ch => (!productQuery.BrandId.HasValue || ch.BrandId == productQuery.BrandId.Value) &&
                   (!productQuery.TypeId.HasValue || ch.TypeId == productQuery.TypeId.Value) &&
                   (string.IsNullOrEmpty(productQuery.SearchValue) || ch.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            
        }
    }
}
