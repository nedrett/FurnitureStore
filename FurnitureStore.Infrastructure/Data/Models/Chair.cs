namespace FurnitureStore.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Data.DataConstants.Chair;

    public class Chair
    {
        [Key]
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
        public string CreatorId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CreatorId))]
        public IdentityUser Creator { get; set; } = null!;

        public string? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public IdentityUser? Buyer { get; set; }
    }
}
