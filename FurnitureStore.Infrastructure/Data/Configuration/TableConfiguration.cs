namespace FurnitureStore.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasData(CreateTable());
        }

        private List<Table> CreateTable()
        {
            var tables = new List<Table>()
            {
                new Table
                {
                    Id = 1,
                    Name = "Dining Table",
                    Material = "Wood",
                    Width = (decimal)2.00,
                    Length = (decimal)0.75,
                    Price = (decimal)800.00,
                    Quantity = 1,
                    Description = "Best Dining Table",
                    ImageUrl = "https://c.media.kavehome.com/images/Products/CC0006M40_1V01.jpg?tx=w_900,c_fill,ar_0.8,q_auto",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            return tables;
        }
    }
}
