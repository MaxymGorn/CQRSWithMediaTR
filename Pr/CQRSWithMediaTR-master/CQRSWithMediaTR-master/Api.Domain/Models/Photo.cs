

namespace Customer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Photo: ModelBase
    {
        public int PhotoId { get; set; }
        public Nullable<int> GoodId { get; set; }
        public string PhotoPath { get; set; }
    }
}
