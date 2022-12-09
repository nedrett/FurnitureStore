namespace FurnitureStore.UnitTests
{
    using Core.Contracts;
    using Core.Models.Furniture.Chair;
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class ChairTests
    {
        private IEnumerable<Chair> Chairs;
        private ApplicationDbContext dbContext;
        private IRepository chairRepo;
        private IChairService chairService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.Chairs = new List<Chair>()
            {
                new Chair()
                {
                    Id = 1,
                    Name = "Dining Chair",
                    Price = (decimal)50.00,
                    Quantity = 4,
                    Description = "Best dining chair",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJbFFCH0CQvVrrVcbKLuQToSX9XW1exfJ3ne1EjgMXyIOvXgCU4XqA4F7BLzAV8RnF1mw&usqp=CAU",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ChairsInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.dbContext.AddRange(this.Chairs);
            this.dbContext.SaveChanges();

            chairRepo = new Repository(this.dbContext);
            chairService = new ChairService(chairRepo);
        }

        [Test]
        public void Test_ChairServiceGetAllReturnsNotNull()
        {
            var resultChairs = chairService.GetAll();

            Assert.True(resultChairs != null);
            Assert.True(resultChairs.Result.Count() == 1);
        }
        
        [Test]
        public void Test_ChairServiceAddChairAddsCorrectProduct()
        {
            var chairToAdd = new ChairModel()
            {
                Id = 2,
                Name = "Test Chair",
                Price = (decimal)20.00,
                Quantity = 4,
                Description = "Best dining chair",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJbFFCH0CQvVrrVcbKLuQToSX9XW1exfJ3ne1EjgMXyIOvXgCU4XqA4F7BLzAV8RnF1mw&usqp=CAU",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            var creatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            chairService.Add(chairToAdd, creatorId);

            var resultChairs = chairService.GetAll();

            Assert.True(resultChairs != null);
            Assert.True(resultChairs.Result.Count() == 2);
        }

        [Test]
        public void Test_ChairServiceDeleteChangesIsActiveFlagToFalse()
        {
            var chairId = 2;

            var chair = chairRepo.GetByIdAsync<Chair>(chairId);

            chairService.Delete(chairId);

            Assert.True(chair.Result.IsActive == false);
        }

        [Test]
        public void Test_ChairServiceChairDetailsByIdReturnsNotNull()
        {
            var chairId = 1;

            var result = chairService.ChairDetailsById(chairId);

            Assert.True(result != null);
        }

        [Test]
        public void Test_ChairServiceChairDetailsByIdReturnsCorrectProduct()
        {
            var chairId = 1;

            var result = chairService.ChairDetailsById(chairId);

            var dbChair = Chairs.FirstOrDefault(c => c.Id == chairId);

            Assert.True(result.Result.Id == dbChair.Id);
            Assert.True(result.Result.Name == dbChair.Name);
            Assert.True(result.Result.Price == dbChair.Price);
            Assert.True(result.Result.Description == dbChair.Description);
        }

        [Test]
        public void Test_ChairServiceEditMakesCorrectChanges()
        {
            var chairId = 2;

            var editedChairName = "Test Chair - Edited";

            var armChairToEdit = new ChairModel()
            {
                Id = 2,
                Name = editedChairName,
                Price = (decimal)20.00,
                Quantity = 4,
                Description = "Best dining chair",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJbFFCH0CQvVrrVcbKLuQToSX9XW1exfJ3ne1EjgMXyIOvXgCU4XqA4F7BLzAV8RnF1mw&usqp=CAU",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            chairService.Edit(chairId, armChairToEdit);

            var dbChair = chairRepo.GetByIdAsync<Chair>(chairId);

            Assert.True(dbChair.Result.Name == editedChairName);
        }

        [Test]
        public void Test_ChairServiceExistReturnsCorrectResult()
        {
            var existingChairId = 1;
            var notExistingChairId = 10;

            var trueResult = chairService.Exists(existingChairId).Result;
            var falseResult = chairService.Exists(notExistingChairId).Result;


            Assert.True(trueResult);
            Assert.True(falseResult == false);
        }
    }
}
