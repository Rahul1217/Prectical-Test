using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical_Test_Interview.Models.Product
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter Product Name.")]
        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
    }
}