using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.UOW;
using ServiceAbstractionLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , IBasketRepo basketRepo) : IServiceManager
    {
        private readonly Lazy<IProductServices> LazyproductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork , mapper));
        public IProductServices ProductServices => LazyproductServices.Value;


        private readonly Lazy<IBasketServices> LazybasketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepo, mapper));
        public IBasketServices BasketServices => LazybasketServices.Value;
        
        
    }
}
