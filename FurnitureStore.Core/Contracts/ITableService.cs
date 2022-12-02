namespace FurnitureStore.Core.Contracts
{
    using Models.Furniture.TvTable;

    public interface ITvTableService
    {
        Task<IEnumerable<TvTableCatalogModel>> GetAll();

        /// <summary>
        /// Add new TvTable item
        /// </summary>
        /// <param name="tvTableModel"></param>
        /// <returns></returns>
        Task Add(TvTableModel tvTableModel, string userId);

        Task Delete(int id);

        Task<TvTableDetailsModel> TvTableDetailsById(int tvTableId);

        Task Edit(int tvTableId, TvTableModel model);

        Task<bool> Exists(int TvTableId);
    }
}
