namespace FurnitureStore.Core.Services.User
{
    using Contracts.User;
    using Infrastructure.Data.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.User;

    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task Forget(string id)
        {
            var user = await repo.GetByIdAsync<IdentityUser>(id);

            user.UserName = null;
            user.NormalizedUserName = null;
            user.Email = null;
            user.NormalizedEmail = null;
            user.PasswordHash = null;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserServiceModel>> All()
        {
            List<UserServiceModel> result;

            result = await repo.AllReadonly<IdentityUser>()
                .Where(u => u.Email != null)
                .Select(u => new UserServiceModel()
                {
                    UserId = u.Id,
                    Email = u.Email
                })
                .ToListAsync();

            return result;
        }
    }
}
