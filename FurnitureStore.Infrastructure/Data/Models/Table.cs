namespace FurnitureStore.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using static Data.DataConstants.Table;

    public class Table
    {
        [Key]
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
        [Precision(18,2)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(int), QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string CreatorId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CreatorId))]
        public IdentityUser Creator { get; set; } = null!;

        public string? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public IdentityUser? Buyer { get; set; }
    }
}
