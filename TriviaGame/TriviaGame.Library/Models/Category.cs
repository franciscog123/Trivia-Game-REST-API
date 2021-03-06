﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.Library.Models
{
    public class Category
    {
        private string _categoryString;
        public int CategoryId { get; set; }
        public string CategoryString
        {
            get => _categoryString;
            set
            {
                if (value.Length==0)
                {
                    throw new ArgumentException("Category must not be empty.", nameof(value));
                }
                _categoryString = value;
            }
        }
    }
}
