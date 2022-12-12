namespace FurnitureStore.UnitTests
{
    using Core.Contracts;
    using Core.Services;
    using Core.Models.Furniture.TvTable;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class TvTableTests
    {
        private IEnumerable<TvTable> TvTables;
        private ApplicationDbContext dbContext;
        private IRepository tvTableRepo;
        private ITvTableService tvTableService;

        [SetUp]
        public void Setup()
        {
            this.TvTables = new List<TvTable>()
            {
                new TvTable
                {
                    Id = 2,
                    Name = "Test Tv Bench",
                    Width = (decimal)2.40,
                    Length = (decimal)0.42,
                    Height = (decimal)0.74,
                    Price = (decimal)175.00,
                    Quantity = 1,
                    Description = "Floor Type Tv Table",
                    ImageUrl = "https://www.ikea.com/nl/en/images/products/besta-tv-bench-with-doors-and-drawers-black-brown-lappviken-stubbarp-black-brown__0719166_pe731898_s5.jpg?f=s",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TvTablesInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            this.dbContext.AddRange(this.TvTables);
            this.dbContext.SaveChanges();

            tvTableRepo = new Repository(this.dbContext);
            tvTableService= new TvTableService(tvTableRepo);
        }

        [Test]
        public void Test_TvTableServiceGetAllReturnsNotNull()
        {
            var resultTable = tvTableService.GetAll();

            Assert.True(resultTable != null);
            Assert.True(resultTable.Result.Count() == 2);
        }
        
        [Test]
        public void Test_TvTableServiceAddTvTableAddsCorrectProduct()
        {
            var tvTableToAdd = new TvTableModel()
            {
                Id = 3,
                Name = "Test Tv Bench",
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Height = (decimal)0.03,
                Price = (decimal)99.00,
                Quantity = 1,
                Description = "Floor Type Tv Table",
                ImageUrl = "https://www.ikea.com/nl/en/images/products/besta-tv-bench-with-doors-and-drawers-black-brown-lappviken-stubbarp-black-brown__0719166_pe731898_s5.jpg?f=s",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            var creatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            tvTableService.Add(tvTableToAdd, creatorId);

            var resultTvTables = tvTableService.GetAll();

            Assert.True(resultTvTables != null);
            Assert.True(resultTvTables.Result.Count() == 3);
        }

        [Test]
        public void Test_TvTableServiceDeleteChangesIsActiveFlagToFalse()
        {
            var tvTableId = 2;

            var tvTable = tvTableRepo.GetByIdAsync<TvTable>(tvTableId);

            tvTableService.Delete(tvTableId);

            Assert.True(tvTable.Result.IsActive == false);
        }

        [Test]
        public void Test_TvTableServiceTvTableDetailsByIdReturnsNotNull()
        {
            var tvTableId = 2;

            var result = tvTableService.TvTableDetailsById(tvTableId);

            Assert.True(result != null);
        }

        [Test]
        public void Test_TvTableServiceTvTableDetailsByIdReturnsCorrectProduct()
        {
            var tvTableId = 2;

            var result = tvTableService.TvTableDetailsById(tvTableId);

            var dbTvTable = TvTables.FirstOrDefault(s => s.Id == tvTableId);

            Assert.True(result.Result.Id == dbTvTable.Id);
            Assert.True(result.Result.Name == dbTvTable.Name);
            Assert.True(result.Result.Price == dbTvTable.Price);
            Assert.True(result.Result.Description == dbTvTable.Description);
        }

        [Test]
        public void Test_TvTableServiceEditMakesCorrectChanges()
        {
            var tvTableId = 2;

            var editedTvTableName = "Test Tv Bench - Edited";

            var tableToEdit = new TvTableModel()
            {
                Id = 2,
                Name = editedTvTableName,
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Height = (decimal)0.03,
                Price = (decimal)99.00,
                Quantity = 1,
                Description = "Floor Type Tv Table",
                ImageUrl = "https://www.ikea.com/nl/en/images/products/besta-tv-bench-with-doors-and-drawers-black-brown-lappviken-stubbarp-black-brown__0719166_pe731898_s5.jpg?f=s",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48"
            };

            tvTableService.Edit(tvTableId, tableToEdit);

            var dbTvTable = tvTableRepo.GetByIdAsync<TvTable>(tvTableId);

            Assert.True(dbTvTable.Result.Name == editedTvTableName);
        }

        [Test]
        public void Test_TvTableServiceExistReturnsCorrectResult()
        {
            var existingTvTableId = 1;
            var notExistingTvTableId = 10;

            var trueResult = tvTableService.Exists(existingTvTableId).Result;
            var falseResult = tvTableService.Exists(notExistingTvTableId).Result;


            Assert.True(trueResult);
            Assert.True(falseResult == false);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
