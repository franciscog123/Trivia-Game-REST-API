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
    /// <summary>
    /// A repository managing data access for User objects and their Quiz members,
    /// using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This class ought to have better exception handling and logging.
    /// </remarks>
    public class UserRepository : IUserRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        /// <summary>
        /// Initializes a new User repository given a suitable User data source.
        /// </summary>
        /// <param name="dbContext">The data source</param>
        /// <param name="logger">The logger</param>
        public UserRepository(TriviaGameDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a list of all entries in the User table. 
        /// If string search is included, it will filter results by UserNames containing "search"
        /// </summary>
        /// <param name="search">The string to filter Users by</param>
        /// <returns>IEnumerable<Library.Models.User></returns>
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

        /// <summary>
        /// Gets a User object from the database with a specific ID
        /// </summary>
        /// <param name="id">The ID of the User to be returned</param>
        /// <returns>Library.Models.User</returns>
        public Library.Models.User GetUserById(int id) =>
            Mapper.Map(_dbContext.User.Include(q => q.Quiz).AsNoTracking().First(u => u.UserId == id));

        /// <summary>
        /// Adds a new User into the database and updates the log
        /// </summary>
        /// <param name="user">The Library model of the User to be added</param>
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

        /// <summary>
        /// Removes a User from the database with a specific ID and updatest the log
        /// </summary>
        /// <param name="id">ID of the User to be deleted</param>
        public void DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with ID {id}");
            User user = _dbContext.User.Find(id);
            _dbContext.Remove(user);
        }
    }
}
