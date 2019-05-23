using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.BL.Models;

namespace TriviaGame.BL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(string search = null);
        User GetUserById(int id);
        void AddUser(User user);
        void DeleteUser(int id);
    }
}
