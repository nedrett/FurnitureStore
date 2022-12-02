namespace FurnitureStore.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TvTableConfiguration : IEntityTypeConfiguration<TvTable>
    {
        public void Configure(EntityTypeBuilder<TvTable> builder)
        {
            builder.HasData(CreateTvTable());
        }

        private List<TvTable> CreateTvTable()
        {
            var tables = new List<TvTable>()
            {
                new TvTable
                {
                    Id = 1,
                    Name = "Tv Bench",
                    Width = (decimal)2.40,
                    Length = (decimal)0.42,
                    Height = (decimal)0.74,
                    Price = (decimal)175.00,
                    Quantity = 1,
                    Description = "Floor Type Tv Table",
                    ImageUrl = "https://www.ikea.com/nl/en/images/products/besta-tv-bench-with-doors-and-drawers-black-brown-lappviken-stubbarp-black-brown__0719166_pe731898_s5.jpg?f=s",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            return tables;
        }
    }
}
