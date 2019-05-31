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

        //public ICategoryRepository CategoryRepo { get; set; }

        //[Fact]
        //public void CategoryRep_getCategories()
        //{
        //    //System.Diagnostics.Debugger.Launch();
        //    IEnumerable<Category> categories = (IEnumerable<Category>)CategoryRepo.GetCategories();
        //    //Assert.Equal("History", category.CategoryString);
        //    //Category category = categories[0];
        //    //Assert.Equal("Movies", category.CategoryString);
        //    int x = 0;
        //    foreach (var category in categories)
        //    {
        //       // Assert.Equal("Movies", category.CategoryString);
        //        x++;
        //    }
        //    Assert.Equal(0, x);
        //}



    }
}
