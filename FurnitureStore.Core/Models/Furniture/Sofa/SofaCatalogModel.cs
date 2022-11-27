using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Core.Models.Furniture.Sofa
{
    public class SofaCatalogModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Display(Name = "Upholstery Type")]
        public string UpholsteryType { get; set; } = null!;
        
        public decimal Price { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        public string CreatorId { get; set; } = null!;

        public bool IsCreator { get; set; }
    }
}
