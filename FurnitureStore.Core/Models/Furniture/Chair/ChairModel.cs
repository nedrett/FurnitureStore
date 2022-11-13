using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FurnitureStore.Infrastructure.Data.DataConstants.Chair;


namespace FurnitureStore.Core.Models.Furniture.Chair
{
    public class ChairModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

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
        public string ImageUrl { get; set; } = null!;
    }
}
