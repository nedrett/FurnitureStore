namespace FurnitureStore.Core.Services
{
    using Contracts;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Furniture.Table;


    public class TableService : ITableService
    {
        private readonly IRepository repo;

        public TableService(IRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Gets All Table Items from Database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TableCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<Table>()
                .Where(t => t.IsActive)
                .Select(t => new TableCatalogModel
                {
                    Id = t.Id,
                    Type = t.Type,
                    Material = t.Material,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl
                }).ToListAsync();
        }

        /// <summary>
        /// Add new Table item
        /// </summary>
        /// <param name="tableModel"></param>
        /// <returns></returns>
        public async Task Add(TableModel tableModel, string userId)
        {
            var tableItem = new Table
            {
                Type = tableModel.Type,
                Material = tableModel.Material,
                Width = tableModel.Width,
                Length = tableModel.Length,
                Price = tableModel.Price,
                Quantity = tableModel.Quantity,
                Description = tableModel.Description,
                ImageUrl = tableModel.ImageUrl,
                CreatorId = userId
            };

            await repo.AddAsync(tableItem);
            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Set IsActive flag to false and removes item from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var table = await repo.GetByIdAsync<Table>(id);

            table.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task<TableDetailsModel> TableDetailsById(int tableId)
        {
            return await repo.AllReadonly<Table>()
                .Where(c => c.IsActive)
                .Where(c => c.Id == tableId)
                .Select(c => new TableDetailsModel
                {
                    Id = c.Id,
                    Type = c.Type,
                    Material = c.Material,
                    Width = c.Width,
                    Length = c.Length,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl
                })
                .FirstAsync();
        }

        public async Task Edit(int tableId, TableModel model)
        {
            var table = await repo.GetByIdAsync<Table>(tableId);
            
            table.Type = model.Type;
            table.Width = model.Width;
            table.Length = model.Length;
            table.Price = model.Price;
            table.Quantity = model.Quantity;
            table.Description = model.Description;
            table.ImageUrl = model.ImageUrl;
            table.Material = model.Material;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int tableId)
        {
            return await repo.AllReadonly<Table>()
                .Where(c => c.IsActive)
                .AnyAsync(c => c.Id == tableId);
        }
    }
}
