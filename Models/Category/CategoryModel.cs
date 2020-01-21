using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical_Test_Interview.Models.Category
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please enter Category Name.")]
        public string CategoryName { get; set; }
    }
}