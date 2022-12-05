using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Core.Models.Furniture
{
    public class ProductViewModel
    {
        public string Name { get; set; } = null!;

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
