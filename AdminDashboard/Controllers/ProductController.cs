using AdminDashboard.Models;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductEntites;
using Microsoft.AspNetCore.Mvc;
using Services.Helper;
using Services.Specifications;
using Shared.ProductModels;
using System.Reflection.Metadata;

namespace AdminDashboard.Controllers
{
    public class ProductController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {

        public async Task <IActionResult> Index()
        {
            var specs = new ProductWithBrandAndTypeSpecifications(new ProductSpecificationsParameters() /*{ PageSize = int.MaxValue }*/);

            var products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(specs);

            var mappedProducts=mapper.Map<IReadOnlyList<ProductResultDTO>>(products);

            return View(mappedProducts);

        }
        [HttpGet]
		public async Task<IActionResult> Create(int id)
        {
            var product=await unitOfWork.GetRepository<Product,int>().GetAsync(id);

            var mappedProduct= mapper.Map<ProductFormViewModel>(product);
            
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductFormViewModel productFormViewModel)
        {
            if(ModelState.IsValid)
            {
                if(productFormViewModel.Picture  != null)
                {
                    productFormViewModel.PictureUrl = await DocumentSettings.UploadFileAsync(productFormViewModel.Picture, "products");
                }
                var mappedProduct = mapper.Map<Product>(productFormViewModel);

                await unitOfWork.GetRepository<Product,int>().AddAsync(mappedProduct);

                await unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(productFormViewModel);
        }




    }
}
