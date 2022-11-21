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

        Task<ChairDetailsModel> ChairDetailsById(int chairId);

        Task Edit(int chairId, ChairModel model);

        Task<bool> Exists(int id);
    }
}
