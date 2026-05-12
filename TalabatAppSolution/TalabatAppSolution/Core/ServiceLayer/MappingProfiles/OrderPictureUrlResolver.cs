using AutoMapper;
using DomainLayer.Models.Orders;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    public class OrderPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ProductItem.PrictureUrl))
            {
                return string.Empty;
            }

            return $"{configuration.GetSection("Urls")["BaseUrl"]}{source.ProductItem.PrictureUrl}";
        }
    }
}
