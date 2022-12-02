namespace FurnitureStore.Core.Services
{
    using Contracts;
    using FurnitureStore.Core.Models.Furniture.TvTable;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class TvTableService : ITvTableService
    {
        private readonly IRepository repo;

        public TvTableService(IRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Gets All TvTable Items from Database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TvTableCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<TvTable>()
                .Where(tt => tt.IsActive)
                .Select(tt => new TvTableCatalogModel
                {
                    Id = tt.Id,
                    Name = tt.Name,
                    Price = tt.Price,
                    ImageUrl = tt.ImageUrl,
                    CreatorId = tt.CreatorId
                }).ToListAsync();
        }

        /// <summary>
        /// Add new TvTable item
        /// </summary>
        /// <param name="tvTableModel"></param>
        /// <returns></returns>
        public async Task Add(TvTableModel tvTableModel, string userId)
        {
            var tvTableItem = new TvTable
            {
                Name = tvTableModel.Name,
                Width = tvTableModel.Width,
                Length = tvTableModel.Length,
                Height = tvTableModel.Height,
                Price = tvTableModel.Price,
                Quantity = tvTableModel.Quantity,
                Description = tvTableModel.Description,
                ImageUrl = tvTableModel.ImageUrl,
                CreatorId = userId
            };

            await repo.AddAsync(tvTableItem);
            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Set IsActive flag to false and removes item from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var tvTable = await repo.GetByIdAsync<TvTable>(id);

            tvTable.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task<TvTableDetailsModel> TvTableDetailsById(int tvTableId)
        {
            return await repo.AllReadonly<TvTable>()
                .Where(tt => tt.IsActive)
                .Where(tt => tt.Id == tvTableId)
                .Select(tt => new TvTableDetailsModel
                {
                    Id = tt.Id,
                    Name = tt.Name,
                    Width = tt.Width,
                    Length = tt.Length,
                    Height = tt.Height,
                    Price = tt.Price,
                    Quantity = tt.Quantity,
                    Description = tt.Description,
                    ImageUrl = tt.ImageUrl,
                    CreatorId = tt.CreatorId
                })
                .FirstAsync();
        }

        public async Task Edit(int tvTableId, TvTableModel model)
        {
            var tvTable = await repo.GetByIdAsync<TvTable>(tvTableId);

            tvTable.Name = model.Name;
            tvTable.Width = model.Width;
            tvTable.Length = model.Length;
            tvTable.Height = model.Height;
            tvTable.Price = model.Price;
            tvTable.Quantity = model.Quantity;
            tvTable.Description = model.Description;
            tvTable.ImageUrl = model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int tvTableId)
        {
            return await repo.AllReadonly<TvTable>()
                .Where(tt => tt.IsActive)
                .AnyAsync(tt => tt.Id == tvTableId);
        }
    }
}
