using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Library.Models;

namespace TriviaGame.Library.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
