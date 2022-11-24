namespace FurnitureStore.Core.Contracts
{
    using Models.Furniture.ArmChair;

    public interface IArmChairService
    {
        Task<IEnumerable<ArmChairCatalogModel>> GetAll();

        /// <summary>
        /// Add new ArmChair item
        /// </summary>
        /// <param name="chairModel"></param>
        /// <returns></returns>
        Task Add(ArmChairModel armChairModel, string userId);

        Task Delete(int id);

        Task<ArmChairDetailsModel> ArmChairDetailsById(int armChairId);

        Task Edit(int armChairId, ArmChairModel model);

        Task<bool> Exists(int armChairId);
    }
}
