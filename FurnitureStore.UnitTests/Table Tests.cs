namespace FurnitureStore.UnitTests
{
    using Core.Contracts;
    using Core.Services;
    using Core.Models.Furniture.Table;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class TableTests
    {
        private IEnumerable<Table> Tables;
        private ApplicationDbContext dbContext;
        private IRepository tableRepo;
        private ITableService tableService;

        [SetUp]
        public void Setup()
        {
            this.Tables = new List<Table>()
            {
                new Table
                {
                    Id = 2,
                    Name = "Test Dining Table",
                    Material = "Wood",
                    Width = (decimal)2.00,
                    Length = (decimal)0.75,
                    Price = (decimal)800.00,
                    Quantity = 1,
                    Description = "Best Dining Table",
                    ImageUrl = "https://c.media.kavehome.com/images/Products/CC0006M40_1V01.jpg?tx=w_900,c_fill,ar_0.8,q_auto",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TablesInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            this.dbContext.AddRange(this.Tables);
            this.dbContext.SaveChanges();

            tableRepo = new Repository(this.dbContext);
            tableService = new TableService(tableRepo);
        }

        [Test]
        public void Test_TableServiceGetAllReturnsNotNull()
        {
            var resultTable = tableService.GetAll();

            Assert.True(resultTable != null);
            Assert.True(resultTable.Result.Count() == 2);
        }
        
        [Test]
        public void Test_TableServiceAddTableAddsCorrectProduct()
        {
            var tableToAdd = new TableModel()
            {
                Id = 3,
                Name = "Test Table",
                Material = "Wood",
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Price = (decimal)111.00,
                Quantity = 1,
                Description = "Best Test Table",
                ImageUrl = "https://c.media.kavehome.com/images/Products/CC0006M40_1V01.jpg?tx=w_900,c_fill,ar_0.8,q_auto",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            var creatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            tableService.Add(tableToAdd, creatorId);

            var resultTables = tableService.GetAll();

            Assert.True(resultTables != null);
            Assert.True(resultTables.Result.Count() == 3);
        }

        [Test]
        public void Test_TableServiceDeleteChangesIsActiveFlagToFalse()
        {
            var tableId = 2;

            var table = tableRepo.GetByIdAsync<Table>(tableId);

            tableService.Delete(tableId);

            Assert.True(table.Result.IsActive == false);
        }

        [Test]
        public void Test_TableServiceTableDetailsByIdReturnsNotNull()
        {
            var tableId = 2;

            var result = tableService.TableDetailsById(tableId);

            Assert.True(result != null);
        }

        [Test]
        public void Test_TableServiceTableDetailsByIdReturnsCorrectProduct()
        {
            var tableId = 2;

            var result = tableService.TableDetailsById(tableId);

            var dbTable = Tables.FirstOrDefault(s => s.Id == tableId);

            Assert.True(result.Result.Id == dbTable.Id);
            Assert.True(result.Result.Name == dbTable.Name);
            Assert.True(result.Result.Material == dbTable.Material);
            Assert.True(result.Result.Price == dbTable.Price);
            Assert.True(result.Result.Description == dbTable.Description);
        }

        [Test]
        public void Test_TableServiceEditMakesCorrectChanges()
        {
            var tableId = 2;

            var editedTableName = "Test Table - Edited";

            var tableToEdit = new TableModel()
            {
                Id = 2,
                Name = editedTableName,
                Material = "Wood",
                Width = (decimal)0.01,
                Length = (decimal)0.02,
                Price = (decimal)111.00,
                Quantity = 1,
                Description = "Best Test Table",
                ImageUrl = "https://c.media.kavehome.com/images/Products/CC0006M40_1V01.jpg?tx=w_900,c_fill,ar_0.8,q_auto",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48"
            };

            tableService.Edit(tableId, tableToEdit);

            var dbTable = tableRepo.GetByIdAsync<Table>(tableId);

            Assert.True(dbTable.Result.Name == editedTableName);
        }

        [Test]
        public void Test_TableServiceExistReturnsCorrectResult()
        {
            var existingTableId = 2;
            var notExistingTableId = 10;

            var trueResult = tableService.Exists(existingTableId).Result;
            var falseResult = tableService.Exists(notExistingTableId).Result;


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
