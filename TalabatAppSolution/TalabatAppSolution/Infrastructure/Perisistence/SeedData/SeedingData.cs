using DomainLayer.Models.ProductModels;
using DomainLayer.Seeding;
using Microsoft.EntityFrameworkCore;
using Perisistence.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Perisistence.SeedData
{
    public class SeedingData : ISeddingData
    {
        private readonly StoreDbContext _storeDbContext;

        public SeedingData(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public void DataSeed()
        {
            if(_storeDbContext.Database.GetPendingMigrations().Any()) 
                _storeDbContext.Database.Migrate();

            if(!_storeDbContext.ProductBrands.Any())
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                var brandsData = File.ReadAllText(@"..\Infrastructure\Perisistence\SeedData\DataForSeeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if(brands is not null && brands.Any())
                {
                    _storeDbContext.ProductBrands.AddRange(brands);
                }
                _storeDbContext.SaveChanges();
            }
            if(!_storeDbContext.ProductTypes.Any())
            {
                var ProductTypesData = File.ReadAllText(@"..\Infrastructure\Perisistence\SeedData\DataForSeeding\types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                if (ProductTypes is not null && ProductTypes.Any())
                {
                    _storeDbContext.ProductTypes.AddRange(ProductTypes);
                }
                _storeDbContext.SaveChanges();
            }
            if(!_storeDbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"..\Infrastructure\Perisistence\SeedData\DataForSeeding\products.json");
                var Products = JsonSerializer.Deserialize<List<Products>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    _storeDbContext.Products.AddRange(Products);
                }
                _storeDbContext.SaveChanges();
            }
        }
    }
}
