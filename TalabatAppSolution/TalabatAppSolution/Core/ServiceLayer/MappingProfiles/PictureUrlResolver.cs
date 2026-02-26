using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Products, ProductDto, string>
    {
        public string Resolve(Products source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

            return $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
