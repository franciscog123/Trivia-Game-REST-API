using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.DataAccess.Repositories;
using TriviaGame.Library.Interfaces;
using Xunit;

namespace TriviaGame.Tests
{
    public class CategoryRepoTest
    {
        private ICategoryRepository GetInMemoryCategoryRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<CategoryRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            DataAccess.Entities.Category cat1 = new DataAccess.Entities.Category()
            {
                Category1 = "Sports",
                CategoryId = 1,
            };
            DataAccess.Entities.Category cat2 = new DataAccess.Entities.Category()
            {
                Category1 = "Movies",
                CategoryId = 2,
            };

            _dbContext.Category.Add(cat1);

            _dbContext.Category.Add(cat2);
            _dbContext.Database.EnsureCreated();
            return new CategoryRepository(_dbContext, _logger);
        }
        [Fact]
        public async void Check_Category()
        {
            ICategoryRepository sut3 = GetInMemoryCategoryRepository();
            IEnumerable<Library.Models.Category> cats = await sut3.GetCategories();
            //var cat1 = cats.ToList();
            //var cat2 = cat1[0].CategoryString;
            //int cat = 2;
            //String cat = await sut.GetCategoryById(3);
            String cat1 = await sut3.GetCategoryById(1);

            //Assert.True(cats.ToList().Count() == 1);
            Assert.True(cat1 == "Sports");
            //Assert.True(cat1 == "Sports");




        }
    }
}
