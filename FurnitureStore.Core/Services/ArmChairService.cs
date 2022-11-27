namespace FurnitureStore.Core.Services
{
    using Contracts;
    using FurnitureStore.Core.Models.Furniture.ArmChair;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class ArmChairService : IArmChairService
    {
        private readonly IRepository repo;

        public ArmChairService(IRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Gets All Chair Items from Database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ArmChairCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<ArmChair>()
                .Where(ac => ac.IsActive)
                .Select(ac => new ArmChairCatalogModel()
                {
                    Id = ac.Id,
                    Name = ac.Name,
                    UpholsteryType = ac.UpholsteryType,
                    Price = ac.Price,
                    ImageUrl = ac.ImageUrl,
                    CreatorId = ac.CreatorId
                }).ToListAsync();
        }

        /// <summary>
        /// Add new Table item
        /// </summary>
        /// <param name="chairModel"></param>
        /// <returns></returns>
        public async Task Add(ArmChairModel armChairModel, string userId)
        {
            var armChairItem = new ArmChair
            {
                Id = armChairModel.Id,
                Name = armChairModel.Name,
                UpholsteryType = armChairModel.UpholsteryType,
                Length = armChairModel.Length,
                Width = armChairModel.Width,
                Height = armChairModel.Height,
                Price = armChairModel.Price,
                Quantity = armChairModel.Quantity,
                Description = armChairModel.Description,
                ImageUrl = armChairModel.ImageUrl,
                CreatorId = userId
            };

            await repo.AddAsync(armChairItem);
            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Set IsActive flag to false and removes item from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var armChair = await repo.GetByIdAsync<ArmChair>(id);

            armChair.IsActive = false;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets Chair Item for Details page
        /// </summary>
        /// <param name="chairId"></param>
        /// <returns></returns>
        public async Task<ArmChairDetailsModel> ArmChairDetailsById(int armChairId)
        {
            return await repo.AllReadonly<ArmChair>()
                .Where(ac => ac.IsActive)
                .Where(ac => ac.Id == armChairId)
                .Select(ac => new ArmChairDetailsModel
                {
                    Id = ac.Id,
                    Name = ac.Name,
                    UpholsteryType = ac.UpholsteryType,
                    Width = ac.Width,
                    Length = ac.Length,
                    Height = ac.Height,
                    Price = ac.Price,
                    Quantity = ac.Quantity,
                    Description = ac.Description,
                    ImageUrl = ac.ImageUrl,
                    CreatorId = ac.CreatorId
                })
                .FirstAsync();
        }

        public async Task Edit(int armChairId, ArmChairModel model)
        {
            var armChair = await repo.GetByIdAsync<ArmChair>(armChairId);

            armChair.Name = model.Name;
            armChair.UpholsteryType = model.UpholsteryType;
            armChair.Width = model.Width;
            armChair.Length = model.Length;
            armChair.Height = model.Height;
            armChair.Price = model.Price;
            armChair.Quantity = model.Quantity;
            armChair.Description = model.Description;
            armChair.ImageUrl = model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await repo.AllReadonly<ArmChair>()
                .Where(ac => ac.IsActive)
                .AnyAsync(ac => ac.Id == id);
        }
    }
}
