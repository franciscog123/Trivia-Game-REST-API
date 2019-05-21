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

        [Fact]
        public void QuestionsByCategory_DefaultValue_Empty()
        {
            // "Empty" would throw an exception if it received a null value.
            // that would result in a failed test as expected, but this way is
            // a bit cleaner.
            Assert.NotNull(category.QuestionsByCategory);
            Assert.Empty(category.QuestionsByCategory);
        }
    }
}
