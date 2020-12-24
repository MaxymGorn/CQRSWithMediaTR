using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.TypeofApiResponce
{
    public class GetServerErrorResponce
    {
        public int code { get; set; }
        public string message { get; set; }
        public string desciption { get; set; }
    }
}
