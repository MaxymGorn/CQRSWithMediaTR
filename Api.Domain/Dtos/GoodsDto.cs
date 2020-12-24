using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Data_Transfer_Objects
{
    public class GoodsDto
    {
        public GoodsDto()
        {

        }
        [JsonProperty("GoodId")]
        public int GoodId { get; set; }
        [JsonProperty("GoodName")]
        public string GoodName { get; set; }
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonProperty("GoodCount")]
        public decimal GoodCount { get; set; }
    }
}
