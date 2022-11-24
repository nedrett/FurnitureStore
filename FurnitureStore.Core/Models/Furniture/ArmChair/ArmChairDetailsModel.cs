namespace FurnitureStore.Core.Models.Furniture.ArmChair
{
    using System.ComponentModel.DataAnnotations;

    public class ArmChairDetailsModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        [Display(Name = "Upholstery Type")]
        public string UpholsteryType { get; set; } = null!;

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal Height { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public string Description { get; set; } = null!;

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
