namespace FurnitureStore.Core.Models.Furniture.Chair
{
    using System.ComponentModel.DataAnnotations;

    public class ChairDetailsModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public string Description { get; set; } = null!;

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
