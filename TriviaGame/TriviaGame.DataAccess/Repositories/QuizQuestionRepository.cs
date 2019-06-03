using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;

namespace TriviaGame.DataAccess.Repositories
{
    public class QuizQuestionRepository:IQuizQuestionRepository
    {

        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<QuizQuestionRepository> _logger;

        public QuizQuestionRepository(TriviaGameDbContext dbContext, ILogger<QuizQuestionRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateQuizQuestion(Library.Models.QuizQuestion quizQuestion)
        {
            if (quizQuestion is null)
            {
                throw new ArgumentNullException(nameof(quizQuestion));
            }
            _logger.LogInformation("Adding quizquestion");
            Entities.QuizQuestion entity = Mapper.Map(quizQuestion);
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return quizQuestion.QuizQuestionId;
        }

        public async Task<Library.Models.QuizQuestion> GetQuizQuestionById(int id)
        {
            try
            {
                var items = _dbContext.QuizQuestion;

                var entity = await items.FirstOrDefaultAsync(x => x.QuizQuestionId == id);
                if (entity is null)
                {
                    return null;
                }
                else
                {
                    return Mapper.Map2(entity);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }


    }
}
