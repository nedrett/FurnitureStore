namespace FurnitureStore.UnitTests
{
    using Core.Contracts;
    using Core.Models.Furniture.ArmChair;
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class ArmChairTests
    {
        private IEnumerable<ArmChair> armChairs;
        private ApplicationDbContext dbContext;
        private IRepository armChairRepo;
        private IArmChairService armChairService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.armChairs = new List<ArmChair>()
            {
                new ArmChair
                {
                    Id = 1,
                    Name = "Roll arm Armchair",
                    UpholsteryType = "Fiber",
                    Width = (decimal)1.00,
                    Length = (decimal)1.00,
                    Height = (decimal)1.30,
                    Price = (decimal)120.00,
                    Quantity = 1,
                    Description = "Best Armchair",
                    ImageUrl = "https://assets.pbimgs.com/pbimgs/rk/images/dp/wcm/202229/0183/burton-upholstered-armchair-navy-c.jpg",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ArmChairsInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.dbContext.AddRange(this.armChairs);
            this.dbContext.SaveChanges();

            armChairRepo = new Repository(this.dbContext);
            armChairService = new ArmChairService(armChairRepo);
        }

        [Test]
        public void Test_ArmChairServiceGetAllReturnsNotNull()
        {
            var resultArmChairs = armChairService.GetAll();
            
            Assert.True(resultArmChairs != null);
            Assert.True(resultArmChairs.Result.Count() == 1);
        }

        [Test]
        public void Test_ArmChairServiceAddArmChairAddsCorrectProduct()
        {
            var armChairToAdd = new ArmChairModel()
            {
                Id = 2,
                Name = "Test Armchair",
                UpholsteryType = "Leather",
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Height = (decimal)0.03,
                Price = (decimal)0.04,
                Quantity = 1,
                Description = "Added for Test Armchair",
                ImageUrl =
                    "https://assets.pbimgs.com/pbimgs/rk/images/dp/wcm/202229/0183/burton-upholstered-armchair-navy-c.jpg",
            };

            var creatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            armChairService.Add(armChairToAdd, creatorId);

            var resultArmChairs = armChairService.GetAll();

            Assert.True(resultArmChairs != null);
            Assert.True(resultArmChairs.Result.Count() == 2);
        }

        [Test]
        public void Test_ArmChairServiceDeleteChangesIsActiveFlagToFalse()
        {
            var armChairId = 2;

            var armChair = armChairRepo.GetByIdAsync<ArmChair>(armChairId);

            armChairService.Delete(armChairId);
            
            Assert.True(armChair.Result.IsActive == false);
        }

        [Test]
        public void Test_ArmChairServiceArmChairDetailsByIdReturnsNotNull()
        {
            var armChairId = 1;

            var result = armChairService.ArmChairDetailsById(armChairId);
            
            Assert.True(result != null);
        }

        [Test]
        public void Test_ArmChairServiceArmChairDetailsByIdReturnsCorrectProduct()
        {
            var armChairId = 1;

            var result = armChairService.ArmChairDetailsById(armChairId);

            var dbArmChair = armChairs.FirstOrDefault(ac => ac.Id == armChairId);

            Assert.True(result.Result.Id == dbArmChair.Id);
            Assert.True(result.Result.Name == dbArmChair.Name);
            Assert.True(result.Result.Length == dbArmChair.Length);
            Assert.True(result.Result.Width == dbArmChair.Width);
            Assert.True(result.Result.Height == dbArmChair.Height);
            Assert.True(result.Result.Price == dbArmChair.Price);
            Assert.True(result.Result.Description == dbArmChair.Description);
        }

        [Test]
        public void Test_ArmChairServiceEditMakesCorrectChanges()
        {
            var armChairId = 2;

            var editedArmChairName = "Test Armchair - Edited";

            var armChairToEdit = new ArmChairModel()
            {
                Id = 2,
                Name = editedArmChairName,
                UpholsteryType = "Leather",
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Height = (decimal)0.03,
                Price = (decimal)0.04,
                Quantity = 1,
                Description = "Added for Test Armchair",
                ImageUrl =
                    "https://assets.pbimgs.com/pbimgs/rk/images/dp/wcm/202229/0183/burton-upholstered-armchair-navy-c.jpg",
            };

            armChairService.Edit(armChairId, armChairToEdit);

            var dbArmChair = armChairRepo.GetByIdAsync<ArmChair>(armChairId);
            
            Assert.True(dbArmChair.Result.Name == editedArmChairName);
        }

        [Test]
        public void Test_ArmChairServiceExistReturnsCorrectResult()
        {
            var existingArmChairId = 1;
            var notExistingArmChairId = 10;

            var trueResult = armChairService.Exists(existingArmChairId).Result;
            var falseResult = armChairService.Exists(notExistingArmChairId).Result;
            

            Assert.True(trueResult);
            Assert.True(falseResult == false);
        }
    }
}
