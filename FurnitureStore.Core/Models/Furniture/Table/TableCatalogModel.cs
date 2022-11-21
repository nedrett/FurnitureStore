using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FurnitureStore.Core.Models.Furniture.Table
{
    public class TableCatalogModel
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;
        
        public string Material { get; set; } = null!;
        
        public decimal Price { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        public string CreatorId { get; set; } = null!;
    }
}
