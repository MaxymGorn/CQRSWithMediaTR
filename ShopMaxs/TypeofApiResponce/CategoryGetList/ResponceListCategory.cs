using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.TypeofApiResponce.CategoryGetList
{
    public class ResponceListCategory
    {
        public int code { get; set; }
        public List<ListCategoryResponce> responce { get; set; }
    }
}
