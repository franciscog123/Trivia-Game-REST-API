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
using System.Threading.Tasks;

namespace TriviaGame.DataAccess.Repositories
{
    /// <summary>
    /// A repository managing data access for Quiz objects and their members using Entity Framework.
    /// </summary>
    public class QuizRepository : IQuizRepository
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
        public IEnumerable<Library.Models.Quiz> GetQuizzes()
        {
            try
            {
                var items = _dbContext.Quiz
                   .Include(qq => qq.QuizQuestion)
                       .ThenInclude(q => q.Question)
                           .ThenInclude(c => c.Choice)
                   .Include(g => g.GameMode)
                   .Include(c => c.Category);
                return Mapper.Map(items);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
            }
        }



        /// <summary>
        /// Get a Quiz by Id, including any associated objects. Updates log with error if action fails.
        /// </summary>
        /// <param name="id">The quiz</param>
        /// <returns></returns>
        public async Task<Library.Models.Quiz> GetQuizById(int id)
        {
            try
            {
                var items = _dbContext.Quiz
                   .Include(qq => qq.QuizQuestion)
                       .ThenInclude(q => q.Question)
                           .ThenInclude(c => c.Choice)
                   .Include(g => g.GameMode)
                   .Include(c => c.Category);
                //Mapper.Map(_dbContext.User.Include(q => q.Quiz).AsNoTracking().First(u => u.UserId == id));
                var entity = await items.FirstAsync(q => q.QuizId == id);
                if(entity is null)
                {
                    return null;
                }
                else
                {
                    return Mapper.Map(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Add a quiz, including any associated objects. Updates log.
        /// </summary>
        /// <param name="quiz">The quiz</param>
        public async Task <int> CreateQuiz (Library.Models.Quiz quiz)
        {
            if (quiz is null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            _logger.LogInformation($"Adding quiz");

            Entities.Quiz entity = Mapper.Map(quiz);
             _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            //update user completed quizzes
            Entities.User userEntity =  _dbContext.User.Where(u => u.UserId == entity.UserId).First();
            if(userEntity!=null)
            {
                userEntity.CompletedQuizzes += 1;
                await _dbContext.SaveChangesAsync();
            }

            return quiz.QuizId;
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
            _dbContext.SaveChanges();
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
                var items = _dbContext.Quiz
                   .Include(qq => qq.QuizQuestion)
                       .ThenInclude(q => q.Question)
                           .ThenInclude(c => c.Choice)
                   .Include(g => g.GameMode)
                   .Include(c => c.Category);
                return Mapper.Map(items.Where(q => q.GameModeId == gameModeId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
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
                var items = _dbContext.Quiz
                   .Include(qq => qq.QuizQuestion)
                       .ThenInclude(q => q.Question)
                           .ThenInclude(c => c.Choice)
                   .Include(g => g.GameMode)
                   .Include(c => c.Category);
                return Mapper.Map(items.Where(q => q.UserId == userId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
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
                var items = _dbContext.Quiz
                   .Include(qq => qq.QuizQuestion)
                       .ThenInclude(q => q.Question)
                           .ThenInclude(c => c.Choice)
                   .Include(g => g.GameMode)
                   .Include(c => c.Category);
                return Mapper.Map(items.Where(q => q.CategoryId == catId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuizzes();
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
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }

        //Todo: havent used this, might delete later
        public async Task<int> CalcTotalScoreByUser(int userId)
        {
            try
            {
                var userQuizzes = GetQuizzesByUserId(userId);
                int total = 0;
                foreach (var Quiz in userQuizzes)
                {
                    total += Quiz.Score;
                }

                return await Task.FromResult(total);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return -1;
            }
        }

        //Created a method to return an empty quiz collection so I don't have to repeat myself
        //in above methods in catch blocks. Done to fix code smell by returning empty collection 
        //instead of null.
        public IEnumerable<Library.Models.Quiz> returnEmptyQuizzes()
            {
                var emptyQuizzes = Enumerable.Empty<Library.Models.Quiz>();
                return emptyQuizzes;
            }

        public async Task<Library.Models.Question> GetRandomQuestion(int categoryId)
        {
            try
            {
                //var randomNumbers = Enumerable.Range(1, 49).OrderBy(x => rnd.Next()).Take(6).ToList();
                List<Library.Models.Question> categoryQuestions = await Task.FromResult(GetQuestionsByCategoryId(categoryId).ToList());
                int numQuestions = categoryQuestions.Count();
                Random rnd = new Random();
                //int questionIndex = rnd.Next(0,numQuestions-1);
                int questionIndex = (Enumerable.Range(0, numQuestions - 1).OrderBy(x => rnd.Next()).Take(1).ToList()[0]);
                return categoryQuestions[questionIndex];
            }
            catch(Exception ex)
            {
                 _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<Library.Models.Question> GetQuestionsByCategoryId(int catId)
        {
            try
            {
                var items = Mapper.Map(_dbContext.Question
                .Include(qc => qc.Choice)
                .Include(c => c.Category).AsNoTracking());
                return items.Where(q => q.CategoryId == catId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return Enumerable.Empty<Library.Models.Question>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Enumerable.Empty<Library.Models.Question>();
            }
        }
    }
}
