using Customer.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Customer.Domain.Commands
{
    public class CreateCategoryCommand : CommandBase<CategoryDto>
    {
        public CreateCategoryCommand()
        {

        }
        [JsonConstructor]
        public CreateCategoryCommand(string CategoryName)
        {
            this.CategoryName = CategoryName;
        }
        [Required]
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
    }
    public class CreateCategoryCommandBackGround : CommandBaseBackGround
    {
        public CreateCategoryCommandBackGround()
        {

        }
        [JsonConstructor]
        public CreateCategoryCommandBackGround(string CategoryName)
        {
            this.CategoryName = CategoryName;
        }
        [Required]
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
    }
}
