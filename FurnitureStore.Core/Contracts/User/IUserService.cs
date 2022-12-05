namespace FurnitureStore.Core.Contracts.User
{
    using Models.User;

    public interface IUserService
    {
        Task Forget(string id);

        Task<IEnumerable<UserServiceModel>> All();
    }
}
