namespace FurnitureStore.UnitTests
{
    using Core.Contracts;
    using Core.Models.Furniture.Sofa;
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class SofaTests
    {
        private IEnumerable<Sofa> Sofas;
        private ApplicationDbContext dbContext;
        private IRepository sofaRepo;
        private ISofaService sofaService;

        [SetUp]
        public void Setup()
        {
            this.Sofas = new List<Sofa>()
            {
                new Sofa
                {
                    Id = 2,
                    Name = "Test Classic Sofa",
                    UpholsteryType = "Leather",
                    Width = (decimal)3.00,
                    Length = (decimal)1.20,
                    Height = (decimal)0.85,
                    Price = (decimal)350.00,
                    Quantity = 1,
                    Description = "Leather 3 Seater Sofa",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShd6D0eit4uZ_i9wgxK1bSQEm-Aqijsi0Tsr-JwxoDfzAJn2F1cgnY6BP7DyTPOdE9g_o&usqp=CAU",
                    CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
                    IsActive = true
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SofasInMemoryDb")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            this.dbContext.AddRange(this.Sofas);
            this.dbContext.SaveChanges();

            sofaRepo = new Repository(this.dbContext);
            sofaService = new SofaService(sofaRepo);
        }

        [Test]
        public void Test_SofaServiceGetAllReturnsNotNull()
        {
            var resultSofas = sofaService.GetAll();

            Assert.True(resultSofas != null);
            Assert.True(resultSofas.Result.Count() == 2);
        }
        
        [Test]
        public void Test_SofaServiceAddSofaAddsCorrectProduct()
        {
            var sofaToAdd = new SofaModel()
            {
                Id = 3,
                Name = "Test Sofa",
                UpholsteryType = "Fiber",
                Width = (decimal)0.02,
                Length = (decimal)0.03,
                Height = (decimal)0.04,
                Price = (decimal)311.00,
                Quantity = 1,
                Description = "Best Test Sofa",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShd6D0eit4uZ_i9wgxK1bSQEm-Aqijsi0Tsr-JwxoDfzAJn2F1cgnY6BP7DyTPOdE9g_o&usqp=CAU",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            var creatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48";

            sofaService.Add(sofaToAdd, creatorId);

            var resultSofas = sofaService.GetAll();

            Assert.True(resultSofas != null);
            Assert.True(resultSofas.Result.Count() == 3);
        }

        [Test]
        public void Test_SofaServiceDeleteChangesIsActiveFlagToFalse()
        {
            var sofaId = 2;

            var sofa = sofaRepo.GetByIdAsync<Sofa>(sofaId);

            sofaService.Delete(sofaId);

            Assert.True(sofa.Result.IsActive == false);
        }

        [Test]
        public void Test_SofaServiceSofaDetailsByIdReturnsNotNull()
        {
            var sofaId = 2;

            var result = sofaService.SofaDetailsById(sofaId);

            Assert.True(result != null);
        }

        [Test]
        public void Test_SofaServiceSofaDetailsByIdReturnsCorrectProduct()
        {
            var sofaId = 2;

            var result = sofaService.SofaDetailsById(sofaId);

            var dbSofa= Sofas.FirstOrDefault(s => s.Id == sofaId);

            Assert.True(result.Result.Id == dbSofa.Id);
            Assert.True(result.Result.Name == dbSofa.Name);
            Assert.True(result.Result.UpholsteryType == dbSofa.UpholsteryType);
            Assert.True(result.Result.Price == dbSofa.Price);
            Assert.True(result.Result.Description == dbSofa.Description);
        }

        [Test]
        public void Test_SofaServiceEditMakesCorrectChanges()
        {
            var sofaId = 2;

            var editedSofaName = "Test Sofa - Edited";

            var sofaToEdit = new SofaModel()
            {
                Id = 2,
                Name = editedSofaName,
                UpholsteryType = "Fiber",
                Width = (decimal)0.02,
                Length = (decimal)0.03,
                Height = (decimal)0.04,
                Price = (decimal)311.00,
                Quantity = 1,
                Description = "Best Test Sofa",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShd6D0eit4uZ_i9wgxK1bSQEm-Aqijsi0Tsr-JwxoDfzAJn2F1cgnY6BP7DyTPOdE9g_o&usqp=CAU",
                CreatorId = "99858a34-d71e-40c5-b550-7f78f07d5a48",
            };

            sofaService.Edit(sofaId, sofaToEdit);

            var dbSofa = sofaRepo.GetByIdAsync<Sofa>(sofaId);

            Assert.True(dbSofa.Result.Name == editedSofaName);
        }

        [Test]
        public void Test_SofaServiceExistReturnsCorrectResult()
        {
            var existingSofaId = 2;
            var notExistingSofaId= 10;

            var trueResult = sofaService.Exists(existingSofaId).Result;
            var falseResult = sofaService.Exists(notExistingSofaId).Result;


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
