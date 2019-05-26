using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Library.Models;
using Xunit;

namespace TriviaGame.Tests
{
    public class CategoryTest
    {
        private readonly Category category = new Category();

        [Fact]
        public void CategoryString_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => category.CategoryString = string.Empty);
        }
    }
}
