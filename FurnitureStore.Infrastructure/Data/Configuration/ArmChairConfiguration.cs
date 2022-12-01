namespace FurnitureStore.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class ArmChairConfiguration : IEntityTypeConfiguration<ArmChair>
    {
        public void Configure(EntityTypeBuilder<ArmChair> builder)
        {
            builder.HasData(CreateArmChair());
        }

        private List<ArmChair> CreateArmChair()
        {
            var armChairs = new List<ArmChair>()
            {
                new ArmChair
                {
                    Id = 1,
                    Name = "Roll arm Armchair",
                    UpholsteryType = "Fiber",
                    Width = (decimal)1.00,
                    Length = (decimal)1.00,
                    Height = (decimal)1.30,
                    Price = (decimal)120.00,
                    Quantity = 1,
                    Description = "Best Armchair",
                    ImageUrl = "https://assets.pbimgs.com/pbimgs/rk/images/dp/wcm/202229/0183/burton-upholstered-armchair-navy-c.jpg",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true,

                },
            };

            return armChairs;
        }
    }
}
