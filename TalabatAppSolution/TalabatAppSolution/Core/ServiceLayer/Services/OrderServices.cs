using AutoMapper;
using Core.DomainLayer.Models.IdentityModels;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Exceptions.BasketExceptions;
using DomainLayer.Exceptions.ProductExceptions;
using DomainLayer.Exceptions.OrdersException;
using DomainLayer.Models.BasketModels;
using DomainLayer.Models.Orders;
using DomainLayer.Models.ProductModels;
using DomainLayer.UOW;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.OrderDtos;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Spceifications;

namespace ServiceLayer.Services
{
    public class OrderServices(IMapper mapper , IUnitOfWork unitOfWork, IBasketRepo basketRepo) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var addressmapper = mapper.Map<AddressDto,OrderAddress>(orderDto.Address);

            var basket = await basketRepo.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);


            ArgumentNullException.ThrowIfNullOrEmpty(basket.PaymentIntentId);
            var spec = new OrderWithPaymentIntentSpeceifications(basket.PaymentIntentId);
            var orderRepo = unitOfWork.GetRepo<Order, Guid>();
            var ExsistOrder = await orderRepo.GetByIdSpecificationsAsync(spec);

            if (ExsistOrder is not null)  orderRepo.Delete(ExsistOrder);

            var prdoucts = unitOfWork.GetRepo<Products, int>();

            List<OrderItem> orderItems = [];

            foreach(var Item in basket.Items)
            {
                var product = await prdoucts.GetByIdAsync(Item.Id) ?? throw new ProductException(Item.Id);

                var ProductItem = new OrderItem()
                {
                    ProductItem = new ProductItemOrdered()
                    {
                        ProductId = Item.Id,
                        ProductName = Item.Name,
                        PrictureUrl = Item.PictureUrl
                    },
                    Quentity = Item.Quantity,
                    Priced = Item.Price
                };
                orderItems.Add(ProductItem);
            }

            var delieveryMethod = await unitOfWork.GetRepo<DeliveryMethod,int>().GetByIdAsync(orderDto.DeliveryMethodId)
                                    ??throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var SubTotal = orderItems.Sum(s => s.Quentity * s.Priced);
            var Order = new Order(email , addressmapper , deliveryMethod : delieveryMethod , 
                                  subTotal : SubTotal , items: orderItems , 
                                  paymentIntentId: basket.PaymentIntentId , status: OrderStatus.PaymentScucced);
            orderRepo.Add(Order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, OrderToReturnDto>(Order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsycn()
        {
            var DeliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethod);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersOfUserAsync(string email)
        {
            var Spec = new OrderSpecification(email);
            var Orders = await unitOfWork.GetRepo<Order, Guid>().GetAllWithSpecificationsAsync(Spec);

            return mapper.Map<IEnumerable<Order> , IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdOfUserAsync(Guid Id)
        {
            var Spec = new OrderSpecification(Id);
            var Order = await unitOfWork.GetRepo<Order, Guid>().GetByIdSpecificationsAsync(Spec);

            return mapper.Map<Order, OrderToReturnDto>(Order);
        }
    }
}
