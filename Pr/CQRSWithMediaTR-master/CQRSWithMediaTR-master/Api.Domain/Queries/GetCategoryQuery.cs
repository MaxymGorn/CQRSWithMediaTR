using Customer.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;

namespace Customer.Domain.Queries
{
    public class GetCategoryQuery : QueryBase<CategoryDto>
    {
        public GetCategoryQuery()
        {

        }
        [JsonConstructor]
        public GetCategoryQuery(int CategoryId)
        {
            this.CategoryId = CategoryId;
        }
        [Required]
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
    }
}
