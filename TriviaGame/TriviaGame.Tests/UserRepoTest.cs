using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
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
    public class UserRepoTest
    {
        private IUserRepository GetInMemoryUserRepository()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<UserRepository>();
            DbContextOptions<TriviaGameDbContext> options;
            //var builder = new DbContextOptionsBuilder<TriviaGameDbContext>();
            //builder.UseInMemoryDatabase(builder, Action<InMemoryDbContextOptionsBuilder>);
            //builder.UseInMemoryDatabase();
            //options = builder.Options;
            options = new DbContextOptionsBuilder<TriviaGameDbContext>().UseInMemoryDatabase(databaseName: "TriviaGameDbContext").Options;
            TriviaGameDbContext _dbContext = new TriviaGameDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            return new UserRepository(_dbContext, _logger);
        }

        [Fact]
        public async void Add_User()
        {
            IUserRepository sut = GetInMemoryUserRepository();
            //IQuizRepository sut1 = GetInMemoryUserRepository();



            Library.Models.User user = new Library.Models.User()
            {
                UserName = "Rodsalomon",
                UserId = 1,
                Email = "fake@hotmail.com",
            };
            Library.Models.User user2 = new Library.Models.User()
            {
                UserName = "Fransalomon",
                UserId = 2,
                Email = "fake2@hotmail.com",
            };



            sut.AddUser(user);
            sut.AddUser(user2);
            

            //Assert.True(sut.OtherGetAllUsers().ToList().Count() == 2);
            Library.Models.User TestUser = await sut.GetUserByEmail("fake@hotmail.com");
            Assert.Equal("Rodsalomon", TestUser.UserName);
            Library.Models.User TestUser2 = sut.GetUserById(2);
            Assert.Equal("Fransalomon", TestUser2.UserName);
        }
    }
}
