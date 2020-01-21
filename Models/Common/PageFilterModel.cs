using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical_Test_Interview.Models.Common
{
    public class PageFilterModel
    {
        public PageFilterModel()
        {
            PageNo = 1;
            PageSize = 10;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int totalpage { get; set; }
        public int totalRecord { get; set; }
    }
}