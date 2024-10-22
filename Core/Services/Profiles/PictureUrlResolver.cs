using AutoMapper;
using Domain.Entities.ProductEntites;
using Microsoft.Extensions.Configuration;
using Shared.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class PictureUrlResolver(IConfiguration configuration)  : IValueResolver<Product, ProductResultDTO, string>
    {
        public string Resolve(Product source, ProductResultDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrWhiteSpace(source.PictureUrl))
                return string.Empty;
            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
