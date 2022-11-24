namespace FurnitureStore.Core.Services
{
    using Contracts;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Furniture.Sofa;


    public class SofaService : ISofaService
    {
        private readonly IRepository repo;

        public SofaService(IRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Gets All Sofa Items from Database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SofaCatalogModel>> GetAll()
        {
            return await repo.AllReadonly<Sofa>()
                .Where(t => t.IsActive)
                .Select(t => new SofaCatalogModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    UpholsteryType = t.UpholsteryType,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl
                }).ToListAsync();
        }

        /// <summary>
        /// Add new Sofa item
        /// </summary>
        /// <param name="sofaModel"></param>
        /// <returns></returns>
        public async Task Add(SofaModel sofaModel, string userId)
        {
            var sofaItem = new Sofa()
            {
                Name = sofaModel.Name,
                UpholsteryType = sofaModel.UpholsteryType,
                Width = sofaModel.Width,
                Length = sofaModel.Length,
                Height = sofaModel.Height,
                Price = sofaModel.Price,
                Quantity = sofaModel.Quantity,
                Description = sofaModel.Description,
                ImageUrl = sofaModel.ImageUrl,
                CreatorId = userId
            };

            await repo.AddAsync(sofaItem);
            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Set IsActive flag to false and removes item from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var sofa = await repo.GetByIdAsync<Sofa>(id);

            sofa.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task<SofaDetailsModel> SofaDetailsById(int sofaId)
        {
            return await repo.AllReadonly<Sofa>()
                .Where(c => c.IsActive)
                .Where(c => c.Id == sofaId)
                .Select(c => new SofaDetailsModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    UpholsteryType = c.UpholsteryType,
                    Width = c.Width,
                    Length = c.Length,
                    Height = c.Height,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl
                })
                .FirstAsync();
        }

        public async Task Edit(int sofaId, SofaModel model)
        {
            var sofa = await repo.GetByIdAsync<Sofa>(sofaId);

            sofa.Name = model.Name;
            sofa.Width = model.Width;
            sofa.Length = model.Length;
            sofa.Height = model.Height;
            sofa.Price = model.Price;
            sofa.Quantity = model.Quantity;
            sofa.Description = model.Description;
            sofa.ImageUrl = model.ImageUrl;
            sofa.UpholsteryType = model.UpholsteryType;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int tableId)
        {
            return await repo.AllReadonly<Sofa>()
                .Where(c => c.IsActive)
                .AnyAsync(c => c.Id == tableId);
        }
    }
}
