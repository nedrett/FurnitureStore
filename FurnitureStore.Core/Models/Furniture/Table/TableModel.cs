using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FurnitureStore.Infrastructure.Data.DataConstants.Table;

namespace FurnitureStore.Core.Models.Furniture.Table
{
    public class TableModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TypeMaxLength, MinimumLength = TypeMinLength)]
        public string Type { get; set; } = null!;

        [Required]
        [StringLength(MaterialMaxLength, MinimumLength = MaterialMinLength)]
        public string Material { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), WidthMinValue, WidthMaxValue)]
        public decimal Width { get; set; }

        [Required]
        [Range(typeof(decimal), LengthMinValue, LengthMaxValue)]
        public decimal Length { get; set; }

        [Required]
        [Precision(18, 2)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(int), QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
