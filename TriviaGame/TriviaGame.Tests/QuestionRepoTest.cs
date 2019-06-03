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
    public class QuestionRepoTest
    {
        private IQuestionRepository GetInMemoryQuestionRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<QuestionRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            //_dbContext.Question.Add(
            //    new DataAccess.Entities.Question()
            //    {
            //        QuestionId = 5,
            //        CategoryId = 1,
            //        Question1 = "Hello?",
            //        Value = 10,

            //    }
            //);
            //DataAccess.Entities.Category cat1 = new DataAccess.Entities.Category()
            //{
            //    Category1 = "Movies",
            //    CategoryId = 1,
            //};
            //_dbContext.Category.Add(cat1);

            _dbContext.Database.EnsureCreated();
            return new QuestionRepository(_dbContext, _logger);
        }
        [Fact]
        public async void Check_GameMode()
        {
            IQuestionRepository sut6 = GetInMemoryQuestionRepository();
            Library.Models.Question qA = new Library.Models.Question
            {
                QuestionId = 4,
                CategoryId = 1,
                QuestionString = "HelloSir?",
                Value = 10,

            };
            int x = await sut6.CreateQuestion(qA);
            //int c1 = await sut5.CreateChoice(choiceA);
            //int c2 = await sut5.CreateChoice(choiceC);
            Library.Models.Question qB = await sut6.GetQuestionById(1);

            //Assert.True(gms.ToList().Count() == 1);
            Assert.True(qA.QuestionString == "HelloSir?");
            //Assert.True(qA.QuestionString == "HelloSir?");
            IEnumerable<Library.Models.Question> qs = sut6.GetQuestionsByCategoryId(1);
            bool hey= await sut6.DeleteQuestion(1);






        }
    }
}

