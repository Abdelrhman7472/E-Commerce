using AdminDashboard.Models;
using AutoMapper;
using Domain.Entities.ProductEntites;
using Services.Profiles;
using Shared.ProductModels;

namespace AdminDashboard.Mapper
{
	public class ProductVMProfile:Profile
	{
		public ProductVMProfile()
		{
			CreateMap<Product, ProductViewModel>().ReverseMap();

			CreateMap<Product, ProductFormViewModel>().ReverseMap();


		}
	}
}
