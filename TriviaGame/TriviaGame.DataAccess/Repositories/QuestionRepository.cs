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
    public class QuestionRepository:IQuestionRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<QuestionRepository> _logger;

        public QuestionRepository(TriviaGameDbContext dbContext, ILogger<QuestionRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Library.Models.Question>> GetQuestions()
        {
            return await Task.FromResult(Mapper.Map(_dbContext.Question
                .Include(qc => qc.Choice)
                .Include(c => c.Category)));
        }

        public async Task<Library.Models.Question> GetQuestionById(int questionId)
        {
            try
            {
                /*var items= Mapper.Map(_dbContext.Question
                .Include(qc => qc.Choice)
                .Include(c => c.Category).AsNoTracking());
                return await Task.FromResult(items.First(q => q.QuestionId == questionId)); original getquestionbyid method
                */
                var items =  _dbContext.Question
                 .Include(qc => qc.Choice)
                 .Include(c => c.Category).AsNoTracking();

                var entity = await items.FirstOrDefaultAsync(x=>x.QuestionId==questionId);

                if (entity is null)
                {
                    return null;
                }
                else
                {
                    return  Mapper.Map(entity);
                }

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
                 return returnEmptyQuestions();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return returnEmptyQuestions();
            }
        }

        public async Task <int>CreateQuestion(Library.Models.Question question)
        {
            if (question is null)
            {
                throw new ArgumentNullException(nameof(question));
                //_logger.LogWarning($"Question to be added has an ID ({question.QuestionId}) already: ignoring.");
            }
            _logger.LogInformation($"Adding question");

            Entities.Question entity = Mapper.Map(question);
            //entity.QuestionId = 0;
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return question.QuestionId;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            _logger.LogInformation($"Deleting Question with ID {id}");
            Entities.Question question = await _dbContext.Question.FindAsync(id);
            if(question == null)
            {
                return false;
            }
            _dbContext.Remove(question);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetLastQuestionAdded()
        {
            try
            {
                return await Task.FromResult(_dbContext.Question.OrderByDescending(x => x.QuestionId).First().QuestionId);
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

        //Created a method to return an empty quiz collection so I don't have to repeat myself
        //in above methods in catch blocks. Done to fix code smell by returning empty collection 
        //instead of null.
        public IEnumerable<Library.Models.Question> returnEmptyQuestions()
        {
            var emptyQuestions = Enumerable.Empty<Library.Models.Question>();
            return emptyQuestions;
        }
    }
}
