using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.Dtos
{
    public class ManufacturerDto
    {
        public ManufacturerDto()
        {

        }
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }
        [JsonProperty("ManufacturerName")]
        public string ManufacturerName { get; set; }
    }
}
