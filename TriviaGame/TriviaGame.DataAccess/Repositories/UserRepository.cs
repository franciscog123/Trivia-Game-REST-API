using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;

namespace TriviaGame.DataAccess.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(TriviaGameDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Library.Models.User> GetUsers(string search = null)
        {
            IQueryable<User> items = _dbContext.User
                .Include(q => q.Quiz);
            if(search != null)
            {
                items = items.Where(u => u.UserName.Contains(search));
            }
            return Mapper.Map(items);
        }

        public Library.Models.User GetUserById(int id) =>
            Mapper.Map(_dbContext.User.Include(q => q.Quiz).AsNoTracking().First(u => u.UserId == id));

        public void AddUser(Library.Models.User user)
        {
            if(user.UserId != 0)
            {
                _logger.LogWarning($"User to be added has an ID ({user.UserId}) already. Ignoring");
            }
            _logger.LogInformation($"Adding user {user.UserName}");

            User entity = Mapper.Map(user);
            entity.UserId = 0;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with ID {id}");
            User user = _dbContext.User.Find(id);
            _dbContext.Remove(user);
        }
    }
}
