namespace FurnitureStore.Core.Models.Furniture.Chair
{
    public class ChairCatalogModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
