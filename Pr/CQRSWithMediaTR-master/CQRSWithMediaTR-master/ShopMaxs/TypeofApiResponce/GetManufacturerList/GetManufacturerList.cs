using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.TypeofApiResponce.GetManufacturerList
{
    public class GetManufacturerList
    {
        public int code { get; set; }
        public List<ListManufacturerResponce> responce { get; set; }
    }
}
