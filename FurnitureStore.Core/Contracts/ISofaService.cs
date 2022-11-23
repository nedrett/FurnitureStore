using FurnitureStore.Core.Models.Furniture.Table;

namespace FurnitureStore.Core.Contracts
{
    public interface ISofaService
    {
        Task<IEnumerable<SofaCatalogModel>> GetAll();

        /// <summary>
        /// Add new Table item
        /// </summary>
        /// <param name="tableModel"></param>
        /// <returns></returns>
        Task Add(SofaModel tableModel, string userId);

        Task Delete(int id);

        Task<SofaDetailsModel> SofaDetailsById(int tableId);

        Task Edit(int tableId, SofaModel model);

        Task<bool> Exists(int tableId);
    }
}
