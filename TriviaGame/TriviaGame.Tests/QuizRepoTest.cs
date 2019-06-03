using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.DataAccess.Repositories;
using TriviaGame.Library.Interfaces;
using Xunit;

namespace TriviaGame.Tests
{
    public class QuizRepoTest
    {
        private IQuizRepository GetInMemoryQuizRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<QuizRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            DataAccess.Entities.User user = new DataAccess.Entities.User()
            {
                UserName = "Rodsalomon",
                UserId = 3,
                Email = "fake@hotmail.com",
            };
            DataAccess.Entities.Category cat1 = new DataAccess.Entities.Category()
            {
                Category1 = "Movies",
                CategoryId = 1,
            };
            DataAccess.Entities.GameMode gm1 = new DataAccess.Entities.GameMode()
            {
                GameMode1 = "Normal",
                GameModeId = 1,
            };
            _dbContext.User.Add(user);
            _dbContext.Category.Add(cat1);
            _dbContext.GameMode.Add(gm1);
            _dbContext.Database.EnsureCreated();
            return new QuizRepository(_dbContext, _logger);
        }

        [Fact]
        public async void Add_Quiz()
        {
            IQuizRepository sut1 = GetInMemoryQuizRepository();
            Library.Models.Quiz quiz1 = new Library.Models.Quiz()
            {
                Score = 10,
                UserId = 3,
                QuizId = 1,
                CategoryId = 1,
                GameModeId = 1,
                Time = DateTime.Now,

            };
            Library.Models.Quiz quiz2 = new Library.Models.Quiz()
            {
                Score = 10,
                UserId = 3,
                QuizId = 2,
                CategoryId = 1,
                GameModeId = 1,
                Time = DateTime.Now,

            };
            Library.Models.Quiz quiz4 = new Library.Models.Quiz()
            {
                Score = 90,
                UserId = 3,
                QuizId = 2,
                CategoryId = 1,
                GameModeId = 1,
                Time = DateTime.Now,

            };

            int x = await sut1.CreateQuiz(quiz1);
            int y = await sut1.CreateQuiz(quiz2);
            //Assert.True(sut1.GetQuizzes().ToList().Count() == 2);
            Library.Models.Quiz quiz3 = await sut1.GetQuizById(2);
            Assert.Equal(10, quiz3.Score);
            bool something = await sut1.EditQuiz(quiz4);
            // Library.Models.Quiz testQuiz = await sut1.GetQuizById(2);
            //Assert.Equal(90, testQuiz.Score);
            //IEnumerable<Library.Models.Quiz> qzs =  sut1.GetQuizzes();
            //IEnumerable<Library.Models.Quiz> qzs1 =  sut1.GetQuizzesByCategoryId(1);
            //IEnumerable<Library.Models.Quiz> qzs2 = sut1.GetQuizzesByGameModeId(1);
            //IEnumerable<Library.Models.Quiz> qzs3 = sut1.GetQuizzesByUserId(3);
            //Library.Models.Question q = await sut1.GetRandomQuestion(1);
            






        }



    }
}
