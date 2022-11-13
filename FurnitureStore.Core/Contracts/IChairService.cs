using FurnitureStore.Core.Models.Furniture.Table;

namespace FurnitureStore.Core.Contracts
{
    using Models.Furniture.Chair;

    public interface IChairService
    {
        Task<IEnumerable<ChairCatalogModel>> GetAll();

        /// <summary>
        /// Add new Chair item
        /// </summary>
        /// <param name="chairModel"></param>
        /// <returns></returns>
        Task Add(ChairModel chairModel, string userId);

        Task Delete(int id);
    }
}
