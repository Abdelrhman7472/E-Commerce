using AutoMapper;
using Domain.Entities.ProductEntites;
using Shared.ProductModels;

namespace AdminDashboard.Mapper
{
	
		public class ProductResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDTO, string>
	{ 
			public string Resolve(Product source, ProductResultDTO destination, string destMember, ResolutionContext context)
			{
				if (string.IsNullOrWhiteSpace(source.PictureUrl))
					return string.Empty;
				//return $"{configuration["BaseUrl"]}{source.PictureUrl}";

				return configuration["BaseUrl"] + source.PictureUrl;
			}
		
	}
}
