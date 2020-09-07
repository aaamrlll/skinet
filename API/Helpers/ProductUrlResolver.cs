using System;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _conf;
        public ProductUrlResolver(IConfiguration conf)
        {
            _conf = conf;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return _conf["ApiUrl"] + source.PictureUrl;
            }
            else
            {
                return null;
            }
        }
    }
}