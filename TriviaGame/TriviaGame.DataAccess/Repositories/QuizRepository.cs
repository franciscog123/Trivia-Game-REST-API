using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Library.Models;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Linq;

namespace TriviaGame.DataAccess.Repositories
{
    /// <summary>
    /// A repository managing data access for Quiz objects and their members using Entity Framework.
    /// </summary>
    public class QuizRepository:IQuizRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<QuizRepository> _logger;

        /// <summary>
        /// Initializes a new quiz repository given a suitable quiz data source.
        /// </summary>
        /// <param name="dbContext">The data source</param>
        /// <param name="logger">The logger</param>
        public QuizRepository(TriviaGameDbContext dbContext, ILogger<QuizRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all quizzes, including any associated objects. Updates log with error if action fails.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The collection of quizzes</returns>
        public IEnumerable<Library.Models.Quiz> GetQuizzes(string search =null)
        {
            try
            {
                return Mapper.Map(_dbContext.Quiz);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get a Quiz by Id, including any associated objects. Updates log with error if action fails.
        /// </summary>
        /// <param name="id">The quiz</param>
        /// <returns></returns>
        public Library.Models.Quiz GetQuizById(int id)
        {
            try
            {
                return Mapper.Map(_dbContext.Quiz.Find(id));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Add a quiz, including any associated objects. Updates log.
        /// </summary>
        /// <param name="quiz">The quiz</param>
        public void AddQuiz(Library.Models.Quiz quiz)
        {
            if (quiz.QuizId != 0)
            {
                _logger.LogWarning($"Quiz to be added has an ID ({quiz.QuizId}) already: ignoring.");
            }

            _logger.LogInformation($"Adding quiz");

            Entities.Quiz entity = Mapper.Map(quiz);
            entity.QuizId = 0;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Removes a Quiz by Id and any associated objects. Updates log.
        /// </summary>
        /// <param name="id">The id of the quiz</param>
        public void DeleteQuiz(int id)
        {
            _logger.LogInformation($"Deleting Quiz with ID {id}");
            Entities.Quiz quiz = _dbContext.Quiz.Find(id);
            _dbContext.Remove(quiz);
        }


        /// <summary>
        /// Gets all quizzes by Game Mode Id. Updates log with error message if action fails.
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns>The collection of quizzes</returns>
        public IEnumerable<Library.Models.Quiz> GetQuizzesByGameModeId(int gameModeId)
        {
            try
            {
                return Mapper.Map(_dbContext.Quiz.Where(q => q.GameModeId == gameModeId));
            }
            catch(SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all quizzes by User Id. Updates log with error message if action fails.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The collection of quizzes</returns>
        public IEnumerable<Library.Models.Quiz> GetQuizzesByUserId(int userId)
        {
            try
            {
                return Mapper.Map(_dbContext.Quiz.Where(q => q.UserId == userId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all quizzes by category id. Updates log with error message if action fails.
        /// </summary>
        /// <param name="catId"></param>
        /// <returns>The collection of quizzes</returns>
        public IEnumerable<Library.Models.Quiz> GetQuizzesByCategoryId(int catId)
        {
            try
            {
                return Mapper.Map(_dbContext.Quiz.Where(q => q.CategoryId == catId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Returns the id of the last quiz added to the database. Updates log if action fails.
        /// </summary>
        /// <returns>The quiz id</returns>
        public int GetLastQuizId()
        {
            try
            {
                return _dbContext.Quiz.OrderByDescending(x => x.QuizId).First().QuizId;
            }
            catch(SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }
    }
}
