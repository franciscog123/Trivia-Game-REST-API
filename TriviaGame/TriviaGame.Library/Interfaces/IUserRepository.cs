using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Library.Models;

namespace TriviaGame.Library.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(string search = null);
        User GetUserById(int id);
        Task<int> AddUser(Library.Models.User user);
        void DeleteUser(int id);
        IEnumerable<ScoreBoard> GetAllScoreboards();
        Task<Library.Models.User> GetUserByEmail(string email);
        Task<Library.Models.User> GetUserAsync(int userId);
        IEnumerable<User> OtherGetAllUsers(string search = null);

        int CalcTotalScoresByUser(int userId);


    }
}
