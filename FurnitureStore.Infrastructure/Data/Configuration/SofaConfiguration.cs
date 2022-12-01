namespace FurnitureStore.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class SofaConfiguration : IEntityTypeConfiguration<Sofa>
    {
        public void Configure(EntityTypeBuilder<Sofa> builder)
        {
            builder.HasData(CreateSofa());
        }

        private List<Sofa> CreateSofa()
        {
            var sofas = new List<Sofa>()
            {
                new Sofa
                {
                    Id = 1,
                    Name = "Classic Sofa",
                    UpholsteryType = "Leather",
                    Width = (decimal)3.00,
                    Length = (decimal)1.20,
                    Height = (decimal)0.85,
                    Price = (decimal)350.00,
                    Quantity = 1,
                    Description = "Leather 3 Seater Sofa",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShd6D0eit4uZ_i9wgxK1bSQEm-Aqijsi0Tsr-JwxoDfzAJn2F1cgnY6BP7DyTPOdE9g_o&usqp=CAU",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            return sofas;
        }
    }
}
