using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.Dtos
{
    public class CategoryDto
    {
        public CategoryDto()
        { 

        }
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }

    }
}
