using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Exceptions.BasketExceptions;
using DomainLayer.Exceptions.OrdersException;
using DomainLayer.Exceptions.ProductExceptions;
using DomainLayer.Models.BasketModels;
using DomainLayer.Models.Orders;
using DomainLayer.Models.ProductModels;
using DomainLayer.UOW;
using Microsoft.Extensions.Configuration;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.BasketDots;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class PaymentServices(IConfiguration configuration, IMapper mapper , IBasketRepo basketRepo , IUnitOfWork unitOfWork) : IPaymentServices
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeConfigurations:SecretKey"];

            var Basket = await basketRepo.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            var ProductRepo = unitOfWork.GetRepo<Products, int>();

            foreach(var item in Basket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductException(item.Id);
                item.Price = product.Price;
            }

            var DeliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                                        ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);
            Basket.ShippingPrice = DeliveryMethod.Price;

            var BasketAmount = (long)(Basket.Items.Sum(i => i.Quantity * i.Price) + DeliveryMethod.Price) * 100;

            var PaymentService = new PaymentIntentService();
            if(Basket.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                var intent = await PaymentService.CreateAsync(options);
                Basket.PaymentIntentId = intent.Id;
                Basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount
                };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, options);
            }

            await basketRepo.CreateOrUpdateBasketAsync(Basket);

            return mapper.Map<CustomerBasket , BasketDto>(Basket);
        }
    }
}
