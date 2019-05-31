using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;

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
        public async Task<IEnumerable<Library.Models.User>> GetUsers(string search = null)
        {
            try
            {
                IQueryable<DataAccess.Entities.User> items = _dbContext.User
              .Include(q => q.Quiz)
                  .ThenInclude(g => g.GameMode)
              .Include(q => q.Quiz)
                  .ThenInclude(qq => qq.QuizQuestion)
                      .ThenInclude(q => q.Question)
                          .ThenInclude(c => c.Choice)
              .Include(q => q.Quiz)
                  .ThenInclude(c => c.Category);
                if (search != null)
                {
                    items = items.Where(u => u.UserName.Contains(search));
                }
                return await Task.FromResult(Mapper.Map(items.AsNoTracking()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
          
        }

        public async Task<Library.Models.User> GetUserByEmail(string email)
        {
            try
            {
                Entities.User user = await _dbContext.User.FirstOrDefaultAsync(a => a.Email.Equals(email));
                if (user == null)
                {
                    throw new ArgumentException();
                }
                return Mapper.Map(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
                
        }

        //copy of GetUsers method above that doesn't use async, 
        //because cant expose enumerators when using task data type to use in GetAllScoreboards method below
        public IEnumerable<Library.Models.User> OtherGetAllUsers(string search =null)
        {
            IQueryable<DataAccess.Entities.User> items = _dbContext.User
              .Include(q => q.Quiz)
                  .ThenInclude(g => g.GameMode)
              .Include(q => q.Quiz)
                  .ThenInclude(qq => qq.QuizQuestion)
                      .ThenInclude(q => q.Question)
                          .ThenInclude(c => c.Choice)
              .Include(q => q.Quiz)
                  .ThenInclude(c => c.Category);
            if (search != null)
            {
                items = items.Where(u => u.UserName.Contains(search));
            }
            return (Mapper.Map(items.AsNoTracking()));
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

            DataAccess.Entities.User entity = Mapper.Map(user);
            entity.UserId = 0;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        //calculates total score for a single user
        public int CalcTotalScoresByUser(int userId)
        {
            try
            {
                var items = _dbContext.Quiz
                       .Include(qq => qq.QuizQuestion)
                           .ThenInclude(q => q.Question)
                               .ThenInclude(c => c.Choice)
                       .Include(g => g.GameMode)
                       .Include(c => c.Category);
                var QuizzesByUser = Mapper.Map(items.Where(q => q.UserId == userId));

                int total = 0;
                foreach (var Quiz in QuizzesByUser)
                {
                    total += Quiz.Score;
                }
                return total;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        //calculates total score for all users and creates a new list of scoreboards
        //sets totalscore and scoreboard class values in list of scoreboards
        //creates a scoreboard object for each user and returns list of scoreboards 
        //in descending order by totalscore
        //its pretty hacky...but it works
        public IEnumerable<ScoreBoard> GetAllScoreboards()
        {
            List<ScoreBoard> boardList = new List<ScoreBoard>();
            ScoreBoard board = new ScoreBoard();
            IEnumerable<Library.Models.User> users = OtherGetAllUsers();
            foreach(var User in users)
            {
                boardList.Add(new ScoreBoard {
                    UserId=User.UserId,
                    UserName=User.UserName,
                    CompletedQuizzes=User.CompletedQuizzes,
                    TotalScore=CalcTotalScoresByUser(User.UserId)
                });;
            }
            return boardList.OrderByDescending(t=>t.TotalScore).ToList();
        }

        /// <summary>
        /// Removes a User from the database with a specific ID and updatest the log
        /// </summary>
        /// <param name="id">ID of the User to be deleted</param>
        public void DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with ID {id}");
            DataAccess.Entities.User user = _dbContext.User.Find(id);
            _dbContext.Remove(user);
        }
    }
}
