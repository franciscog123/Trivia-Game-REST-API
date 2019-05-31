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
        void AddUser(User user);
        void DeleteUser(int id);
        IEnumerable<ScoreBoard> GetAllScoreboards();
        Task<Library.Models.User> GetUserByEmail(string email);

        IEnumerable<User> OtherGetAllUsers(string search = null);


    }
}
