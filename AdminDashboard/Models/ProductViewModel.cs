using Domain.Entities.ProductEntites;

namespace AdminDashboard.Models
{
	public class ProductViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int BrandId { get; set; }
		public ProductBrand? ProductBrand { get; set; }

		public int TypeId { get; set; }
		public ProductType? ProductType { get; set; }

	}
}
