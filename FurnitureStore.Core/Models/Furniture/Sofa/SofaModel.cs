using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FurnitureStore.Infrastructure.Data.DataConstants.Sofa;

namespace FurnitureStore.Core.Models.Furniture.Sofa
{
    public class SofaModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(UpholsteryTypeMaxLength, MinimumLength = UpholsteryTypeMinLength)]
        [Display(Name = "Upholstery Type")]

        public string UpholsteryType { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), WidthMinValue, WidthMaxValue)]
        public decimal Width { get; set; }

        [Required]
        [Range(typeof(decimal), LengthMinValue, LengthMaxValue)]
        public decimal Length { get; set; }

        [Required]
        [Range(typeof(decimal), HeightMinValue, HeightMaxValue)]
        public decimal Height { get; set; }

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

        public string CreatorId { get; set; } = null!;
    }
}
