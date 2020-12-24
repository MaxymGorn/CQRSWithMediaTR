using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.TypeofApiResponce.GetGoodList
{
    public class GetListGoods
    {
        public int code { get; set; }
        public List<ListGoodResponce> responce { get; set; }
    }
}
