using FurnitureStore.Core.Models.Furniture.Table;

namespace FurnitureStore.Core.Contracts
{
    public interface ITableService
    {
        Task<IEnumerable<TableCatalogModel>> GetAll();

        /// <summary>
        /// Add new Table item
        /// </summary>
        /// <param name="tableModel"></param>
        /// <returns></returns>
        Task Add(TableModel tableModel, string userId);

        Task Delete(int id);
    }
}
