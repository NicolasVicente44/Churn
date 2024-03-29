﻿using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Churn.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide a category name."), MaxLength(200)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Category Description")]
        public string? Description { get; set; }
        public string? Icon { get; set; }

        public List<Product>? Products { get; set; }//child reference

    }
}
