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
    public class QuestionRepository:IQuestionRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<QuestionRepository> _logger;

        public QuestionRepository(TriviaGameDbContext dbContext, ILogger<QuestionRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Library.Models.Question GetQuestionById(int questionId)
        {
            try
            {
                return Mapper.Map(_dbContext.Question.Find(questionId));
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

        public IEnumerable<Library.Models.Question> getQuestionsByCategoryId(int catId)
        {
            try
            {
                return Mapper.Map(_dbContext.Question.Where(q => q.CategoryId == catId));
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

        public void AddQuestion(Library.Models.Question question)
        {
            if (question.QuestionId != 0)
            {
                _logger.LogWarning($"Question to be added has an ID ({question.QuestionId}) already: ignoring.");
            }

            _logger.LogInformation($"Adding question");

            Entities.Question entity = Mapper.Map(question);
            entity.QuestionId = 0;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteQuestion(int id)
        {
            _logger.LogInformation($"Deleting Question with ID {id}");
            Entities.Question question = _dbContext.Question.Find(id);
            _dbContext.Remove(question);
            _dbContext.SaveChanges();
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
