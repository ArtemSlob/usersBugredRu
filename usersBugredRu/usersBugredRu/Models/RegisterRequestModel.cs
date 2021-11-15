using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usersBugredRu.Models
{
    class RegisterRequestModel
    {
        public class RegistrationRequestModel
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        //public class CreateCompanyRequestModel
        //{
        //    [JsonProperty("company_name")]
        //    public string CompanyName { get; set; }

        //    [JsonProperty("company_type")]
        //    public string CompanyType { get; set; }

        //    [JsonProperty("company_users")]
        //    public List<string> CompanyUsers { get; set; }

        //    [JsonProperty("email_owner")]
        //    public string EmailOwner { get; set; }
        //}

        public class CreateCompanyRequestModel
        {
            [JsonProperty("company_name")]
            public string CompanyName { get; set; }

            [JsonProperty("company_type")]
            public string CompanyType { get; set; }

            [JsonProperty("company_users")]
            public List<string> CompanyUsers { get; set; }

            [JsonProperty("email_owner")]
            public string EmailOwner { get; set; }
        }
    }
}
