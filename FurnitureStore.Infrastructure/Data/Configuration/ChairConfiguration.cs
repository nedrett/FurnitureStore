namespace FurnitureStore.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class ChairConfiguration : IEntityTypeConfiguration<Chair>
    {
        public void Configure(EntityTypeBuilder<Chair> builder)
        {
            builder.HasData(CreateChair());
        }

        private List<Chair> CreateChair()
        {
            var chairs = new List<Chair>()
            {
                new Chair()
                {
                    Id = 1,
                    Name = "Dining Chair",
                    Price = (decimal)50.00,
                    Quantity = 4,
                    Description = "Best dining chair",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJbFFCH0CQvVrrVcbKLuQToSX9XW1exfJ3ne1EjgMXyIOvXgCU4XqA4F7BLzAV8RnF1mw&usqp=CAU",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            return chairs;
        }
    }
}
