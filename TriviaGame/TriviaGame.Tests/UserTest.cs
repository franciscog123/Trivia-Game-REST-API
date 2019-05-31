using System;
using TriviaGame.Library.Models;
using Xunit;

namespace TriviaGame.Tests
{
    public class UserTest
    {
        [Fact]
        public void UserNameTooLong()
        {
            var user = new User();
            var name = "My name is fred. I have no head. I've lived this way my whole life now, I thought I would be dead";

            Assert.Throws<ArgumentException>(() => user.UserName = name);
        }
        [Fact]
        public void UserNameEmpty()
        {
            var user = new User();
            var name = "";

            Assert.Throws<ArgumentException>(() => user.UserName = name);
        }
    }
}
