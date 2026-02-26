using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Spceifications
{
    public class ProductSpecification : BasicSpecification<Products , int>
    {
        public ProductSpecification():base(null)
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }
        public ProductSpecification(int id) : base(ch => ch.Id == id)
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }

    }
}
