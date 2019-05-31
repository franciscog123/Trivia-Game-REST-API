using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Library.Models;
using Xunit;
using TriviaGame.DataAccess.Repositories;
using TriviaGame.Library.Interfaces;

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
        public void CategoryId_get()
        {
            Category category = new Category();
            category.CategoryId = 1;
            Assert.Equal(1, category.CategoryId);
        }
        [Fact]
        public void CategoryString_get()
        {
            Category category = new Category();
            category.CategoryString = "History";
            Assert.Equal("History", category.CategoryString);
        }
    }
}
