using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Core.Models.Furniture
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
