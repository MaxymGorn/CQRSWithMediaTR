using System.ComponentModel.DataAnnotations;
using Customer.Domain.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Customer.Domain.Commands
{
    public class CreateCustomerCommand : CommandBase<CustomerDto>
    {
        public CreateCustomerCommand()
        {
        }

        [JsonConstructor]
        public CreateCustomerCommand(string name, string email, string address, int age, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            Age = age;
            PhoneNumber = phoneNumber;
        }
        [Required]
        [JsonProperty("name")]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [JsonProperty("email")]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonProperty("address")]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [JsonProperty("age")]
        public int Age { get; set; }

        [Required]
        [JsonProperty("phone_number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}