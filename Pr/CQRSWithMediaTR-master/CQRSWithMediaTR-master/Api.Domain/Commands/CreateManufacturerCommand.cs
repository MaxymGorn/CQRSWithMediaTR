using Customer.Domain.Dtos;
using Customer.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Customer.Domain.Commands
{
    public class CreateManufacturerCommand : CommandBase<ManufacturerDto>
    {
        public CreateManufacturerCommand()
        {
        }
        [JsonConstructor]
        public CreateManufacturerCommand(string ManufacturerName)
        {
            this.ManufacturerName = ManufacturerName;
        }
        [Required]
        [JsonProperty("ManufacturerName")]
        public string ManufacturerName { get; set; }
    }
    public class CreateManufacturerCommandBackGround : CommandBaseBackGround
    {
        public CreateManufacturerCommandBackGround()
        {
        }
        [JsonConstructor]
        public CreateManufacturerCommandBackGround(string ManufacturerName)
        {
            this.ManufacturerName = ManufacturerName;
        }
        [Required]
        [JsonProperty("ManufacturerName")]
        public string ManufacturerName { get; set; }
    }
}
