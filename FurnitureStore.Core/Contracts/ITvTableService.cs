namespace FurnitureStore.Core.Contracts
{
    using Models.Furniture.Table;

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

        Task<TableDetailsModel> TableDetailsById(int tableId);

        Task Edit(int tableId, TableModel model);

        Task<bool> Exists(int tableId);
    }
}
