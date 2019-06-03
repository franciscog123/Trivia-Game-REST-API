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
    public class GameModeRepoTest
    {
        private IGameModeRepository GetInMemoryGameModeRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<GameModeRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.GameMode.AddRange(
                new DataAccess.Entities.GameMode()
                {
                    GameMode1 = "Normal",
                    GameModeId = 1,
                },
                new DataAccess.Entities.GameMode()
                {
                    GameMode1 = "Hard",
                    GameModeId = 2,
                }
            );

            //_dbContext.GameMode.AddRange(gm1, gm2);
            //_dbContext.GameMode.Add(gm2);
            _dbContext.Database.EnsureCreated();
            return new GameModeRepository(_dbContext, _logger);
        }
        [Fact]
        public async void Check_GameMode()
        {
            IGameModeRepository sut4 = GetInMemoryGameModeRepository();
            IEnumerable<Library.Models.GameMode> gms = await sut4.GetGameModes();
            //var cat1 = cats.ToList();
            //var cat2 = cat1[0].CategoryString;
            //int cat = 2;
            //String cat = await sut.GetCategoryById(3);
            String gm1 = await sut4.GetGameModeById(1);
            String gm2 = await sut4.GetGameModeById(2);

            //Assert.True(gms.ToList().Count() == 1);
            Assert.True(gm1 == "Normal");
            //Assert.True(gm2 == "Hard");




        }
    }
}