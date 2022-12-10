using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public int Quantity { get; set; }

        public string? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public IdentityUser? Buyer { get; set; }
    }
}
