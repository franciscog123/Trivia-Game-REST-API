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
    public class ChoiceRepoTest
    {
        private IChoiceRepository GetInMemoryChoiceRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<ChoiceRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Question.Add(
                new DataAccess.Entities.Question()
                {
                    QuestionId = 5,
                    CategoryId = 1,
                    Question1 = "Hello?",
                    Value = 10,

                }
            );
            //DataAccess.Entities.Category cat1 = new DataAccess.Entities.Category()
            //{
            //    Category1 = "Movies",
            //    CategoryId = 1,
            //};
            //_dbContext.Category.Add(cat1);

            _dbContext.Database.EnsureCreated();
            return new ChoiceRepository(_dbContext, _logger);
        }
        [Fact]
        public async void Check_GameMode()
        {
            IChoiceRepository sut5 = GetInMemoryChoiceRepository();
            Library.Models.Choice choiceA = new Library.Models.Choice
            {
                ChoiceId = 1,
                QuestionId = 5,
                Correct = true,
                ChoiceString = "Hello"

            };
            int c1 = await sut5.CreateChoice(choiceA);
            //int c2 = await sut5.CreateChoice(choiceC);
            Library.Models.Choice choiceB = await sut5.GetChoiceById(1);

            //Assert.True(gms.ToList().Count() == 1);
            Assert.True(choiceB.ChoiceString == "Hello");
            //Assert.True(gm2 == "Hard");
            IEnumerable<Library.Models.Choice> choices = await sut5.GetChoicesByQuestionId(1);
            //Assert.True(choices.ToList().Count() == 2);



        }
    }
}
