namespace FurnitureStore.UnitTests
{
    using Core.Contracts.User;
    using Core.Services.User;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;


    public class UserTests
    {
        private IEnumerable<IdentityUser> Users;
        private ApplicationDbContext dbContext;
        private IRepository userRepo;
        private IUserService userService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.Users = new List<IdentityUser>()
            {
                new IdentityUser()
                {
                    Id = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    UserName = "user@mail.com",
                    NormalizedUserName = "USER@MAIL.COM",
                    Email = "user@mail.com",
                    NormalizedEmail = "USER@MAIL.COM"
                }
                
        };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.dbContext.AddRange(this.Users);
            this.dbContext.SaveChanges();

            userRepo = new Repository(this.dbContext);
            userService = new UserService(userRepo);
        }

        [Test]
        public void Test_UserServiceAllReturnsNotNull()
        {
            var resultUsers = userService.All();

            Assert.True(resultUsers != null);
            Assert.True(resultUsers.Result.Count() == 1);
        }

        [Test]
        public void Test_UserServiceForgetSetsCorrectValues()
        {
            var userId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            userService.Forget(userId);

            var dbUser = userRepo.GetByIdAsync<IdentityUser>(userId).Result;

            Assert.True(dbUser.UserName == null);
            Assert.True(dbUser.NormalizedUserName == null);
            Assert.True(dbUser.Email == null);
            Assert.True(dbUser.NormalizedEmail == null);
        }
    }
}
