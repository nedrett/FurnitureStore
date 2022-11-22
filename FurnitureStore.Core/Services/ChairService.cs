namespace FurnitureStore.Core.Services
{
    using Contracts;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Furniture.Chair;


    public class ChairService : IChairService
    {
        private readonly IRepository repo;

        public ChairService(IRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Gets All Chair Items from Database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ChairCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<Chair>()
                .Where(c => c.IsActive)
                .Select(c => new ChairCatalogModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl
                }).ToListAsync();
        }

        /// <summary>
        /// Add new Table item
        /// </summary>
        /// <param name="chairModel"></param>
        /// <returns></returns>
        public async Task Add(ChairModel chairModel, string userId)
        {
            var chairItem = new Chair
            {
                Id = chairModel.Id,
                Name = chairModel.Name,
                Price = chairModel.Price,
                Quantity = chairModel.Quantity,
                Description = chairModel.Description,
                ImageUrl = chairModel.ImageUrl,
                CreatorId = userId
            };

            await repo.AddAsync(chairItem);
            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Set IsActive flag to false and removes item from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var chair = await repo.GetByIdAsync<Chair>(id);

            chair.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets Chair Item for Details page
        /// </summary>
        /// <param name="chairId"></param>
        /// <returns></returns>
        public async Task<ChairDetailsModel> ChairDetailsById(int chairId)
        {
            return await repo.AllReadonly<Chair>()
                .Where(c => c.IsActive)
                .Where(c => c.Id == chairId)
                .Select(c => new ChairDetailsModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl
                })
                .FirstAsync();
        }

        public async Task Edit(int chairId, ChairModel model)
        {
            var chair = await repo.GetByIdAsync<Chair>(chairId);

            chair.Name = model.Name;
            chair.Price = model.Price;
            chair.Quantity = model.Quantity;
            chair.Description = model.Description;
            chair.ImageUrl = model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int chairId)
        {
            return await repo.AllReadonly<Chair>()
                .Where(c => c.IsActive)
                .AnyAsync(c => c.Id == chairId);
        }
    }
}
