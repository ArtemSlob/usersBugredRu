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

        public class CreateTaskRequestModel
        {
            [JsonProperty("task_title")]
            public string TaskTitle { get; set; }

            [JsonProperty("task_description")]
            public string TaskDescription { get; set; }

            [JsonProperty("email_owner")]
            public string EmailOwner { get; set; }

            [JsonProperty("email_assign")]
            public string EmailAssign { get; set; }
        }

        public class CreateUserRequestModel
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("tasks")]
            public List<int> Tasks { get; set; }

            [JsonProperty("companies")]
            public List<int> Companies { get; set; }

            [JsonProperty("hobby")]
            public string Hobby { get; set; }

            [JsonProperty("adres")]
            public string Adres { get; set; }

            [JsonProperty("name1")]
            public string Name1 { get; set; }

            [JsonProperty("surname1")]
            public string Surname1 { get; set; }

            [JsonProperty("fathername1")]
            public string Fathername1 { get; set; }

            [JsonProperty("cat")]
            public string Cat { get; set; }

            [JsonProperty("dog")]
            public string Dog { get; set; }

            [JsonProperty("parrot")]
            public string Parrot { get; set; }

            [JsonProperty("cavy")]
            public string Cavy { get; set; }

            [JsonProperty("hamster")]
            public string Hamster { get; set; }

            [JsonProperty("squirrel")]
            public string Squirrel { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("inn")]
            public string Inn { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("birthday")]
            public string Birthday { get; set; }

            [JsonProperty("date_start")]
            public string DateStart { get; set; }
        }
    }
}
