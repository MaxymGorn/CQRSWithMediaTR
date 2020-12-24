using Newtonsoft.Json;
using Shop.Domain.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Customer.Domain.Commands
{
    public class CreateGoodsCommand : CommandBase<GoodsDto>
    {
        public CreateGoodsCommand()
        {
        }
        [JsonConstructor]
        public CreateGoodsCommand(string GoodName, int ManufacturerId, int CategoryId, decimal Price, decimal GoodCount)
        {
            this.CategoryId = CategoryId;
            this.ManufacturerId = ManufacturerId;
            this.CategoryId = CategoryId;
            this.Price = Price;
            this.GoodCount = GoodCount;
            this.GoodName = GoodName;
        }
        [Required]
        [JsonProperty("GoodName")]
        public string GoodName { get; set; }
        [Required]
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }
        [Required]
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [Required]
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [Required]
        [JsonProperty("GoodCount")]
        public decimal GoodCount { get; set; }
    }
    public class CreateGoodsCommandBackGround : CommandBaseBackGround
    {
        public CreateGoodsCommandBackGround()
        {
        }
        [JsonConstructor]
        public CreateGoodsCommandBackGround(string GoodName, int ManufacturerId, int CategoryId, decimal Price, decimal GoodCount)
        {
            this.CategoryId = CategoryId;
            this.ManufacturerId = ManufacturerId;
            this.CategoryId = CategoryId;
            this.Price = Price;
            this.GoodCount = GoodCount;
            this.GoodName = GoodName;
        }
        [Required]
        [JsonProperty("GoodName")]
        public string GoodName { get; set; }
        [Required]
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }
        [Required]
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [Required]
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [Required]
        [JsonProperty("GoodCount")]
        public decimal GoodCount { get; set; }
    }
}
