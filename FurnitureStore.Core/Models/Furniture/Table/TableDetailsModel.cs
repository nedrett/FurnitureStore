using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Core.Models.Furniture.Table
{
    public class TableDetailsModel
    {
        public int Id { get; set; }
        
        public string Type { get; set; } = null!;
        
        public string Material { get; set; } = null!;
        
        public decimal Width { get; set; }
        
        public decimal Length { get; set; }
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        public string Description { get; set; } = null!;
        
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
