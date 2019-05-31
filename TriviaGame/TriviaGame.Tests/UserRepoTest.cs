using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriviaGame.DataAccess.Entities;
using TriviaGame.DataAccess.Repositories;
using TriviaGame.Library.Interfaces;
using Xunit;

namespace TriviaGame.Tests
{
    public class UserRepoTest
    {
        private readonly ILogger<UserRepository> _logger;
        private IUserRepository GetInMemoryUserRepository()
        {
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
        public void Add_WhenHaveNoEmail()
        {
            IUserRepository sut = GetInMemoryUserRepository();
            Library.Models.User user = new Library.Models.User()
            {
                UserName = "Rodsalomon",
                UserId = 1,
               // Surname = "Blogs"
            };

            sut.AddUser(user);
            //IEnumerable < user > =

            Assert.True(sut.OtherGetAllUsers().ToList().Count() == 1);
            //Assert.Equal("fred", savedPerson.FirstName);
            //Assert.Equal("Blogs", savedPerson.Surname);
            //Assert.Null(savedPerson.EmailAddresses);
        }
    }
}
