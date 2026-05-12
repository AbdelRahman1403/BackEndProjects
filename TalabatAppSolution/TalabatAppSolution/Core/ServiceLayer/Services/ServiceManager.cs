using AutoMapper;
using Core.DomainLayer.Models.IdentityModels;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstractionLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , IBasketRepo basketRepo , UserManager<ApplicationUser> userManager , IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductServices> LazyproductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork , mapper));
        public IProductServices ProductServices => LazyproductServices.Value;


        private readonly Lazy<IBasketServices> LazybasketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepo, mapper));
        public IBasketServices BasketServices => LazybasketServices.Value;

        private readonly Lazy<IAuthenticationServices> LazyauthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager , configuration , mapper));
        public IAuthenticationServices AuthenticationServices => LazyauthenticationServices.Value;

        private readonly Lazy<IOrderServices> LazyOrderServices = new Lazy<IOrderServices>(() => new OrderServices(mapper , unitOfWork , basketRepo));
        public IOrderServices OrderServices => LazyOrderServices.Value;

        private readonly Lazy<IPaymentServices> LazyPaymetServices = new Lazy<IPaymentServices>(() => new PaymentServices(configuration , mapper , basketRepo , unitOfWork));
        public IPaymentServices paymentServices => LazyPaymetServices.Value;
    }
}
