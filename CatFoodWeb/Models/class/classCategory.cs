using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatFoodWeb.Models
{
    public class classCategory
    {
        public int fCategoryID { get; set; }

        public string fCategoryName { get; set; }

        public string fCategoryDescription { get; set; }

        public int fSuggestOrderQty { get; set; }

        public int fSuggestMakeQty { get; set; }
        public int fTotal { get; set; }
    }
}