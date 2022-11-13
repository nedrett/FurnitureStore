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

        public async Task<IEnumerable<ChairCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<Chair>()
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

        public async Task Delete(int id)
        {
            var chair = await repo.All<Chair>()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chair != null)
            {
                repo.Delete(chair);

                await repo.SaveChangesAsync();
            }
        }
    }
}
