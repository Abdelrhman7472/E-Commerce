using AutoMapper;
using Domain.Entities.ProductEntites;
using Shared.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResultDTO>()
              //ForMember: Destination el awl ba3dha el source
                .ForMember(d => d.BrandName, options=>options.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.TypeName,options=>options.MapFrom(s=>s.ProductType.Name))
                .ForMember(d => d.PictureUrl,options=>options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, BrandResultDTO>();
            CreateMap<ProductType, TypeResultDTO>();
        }
    }
}
