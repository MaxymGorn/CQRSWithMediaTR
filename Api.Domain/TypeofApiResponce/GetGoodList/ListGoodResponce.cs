using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.TypeofApiResponce.GetGoodList
{
    public class ListGoodResponce
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public int GoodCount { get; set; }
    }
}
