using Customer.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Customer.Domain.Queries
{
    public class GetManufacturerQuery : QueryBase<ManufacturerDto>
    {
        public GetManufacturerQuery()
        {

        }
        [JsonConstructor]
        public GetManufacturerQuery(int ManufacturerId)
        {
            this.ManufacturerId = ManufacturerId;
        }
        [Required]
        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }

    }
}
