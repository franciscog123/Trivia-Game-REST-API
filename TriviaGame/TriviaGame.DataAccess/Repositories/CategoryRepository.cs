using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;

namespace TriviaGame.DataAccess.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {

        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(TriviaGameDbContext dbContext, ILogger<CategoryRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Library.Models.Category>> GetCategories()
        {

            return await Task.FromResult(Mapper.Map(_dbContext.Category));
        }
    }
}
