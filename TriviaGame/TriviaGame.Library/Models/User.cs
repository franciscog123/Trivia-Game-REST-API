using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaGame.Library.Models
{
    /// <summary>
    /// A User object. Has an ID, username, email, and list of completed quizzes
    /// </summary>
    public class User
    {
        private string _userName;
        private string _email;

        public int UserId { get; set; }
        public string UserName
        {
            get => _userName;
            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentException("Username must be between 0 and 20 characters", nameof(value));
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Username cannot be empty", nameof(value));
                }
                _userName = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (new EmailAddressAttribute().IsValid(value))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("email is not valid", nameof(value));
                }
            }
        }
        public int? CompletedQuizzes { get; set; }
        public List<Quiz> Quizzes { get; set; }
    }
}
