using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static usersBugredRu.Models.RegisterRequestModel;

namespace usersBugredRu.Helpers
{
    class Helper
    {
        public string DateTimeNowString = DateTime.Now.ToString("ddMMyyyyhhmmss");
        public static Random RandomNumber = new Random();

        public bool CheckResponseForAllCompanyUsers(List<string> companyUsersRequest, string companyUsersResponse)
        {
            foreach (string user in companyUsersRequest)
            {
                if (!companyUsersResponse.Contains(user))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckResponseForIntDataFromRequest(List<int> intNumbers, string stringResponse)
        {
            foreach (int number in intNumbers)
            {
                if (!stringResponse.Contains(number.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        public string NewUserEmail()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/doregister");
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = RandomNumber.Next() + "user" + DateTimeNowString + "@gmail.com",
                Name = RandomNumber.Next() + "User" + DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            return jsonResponse["email"].ToString();
        }

        public int NewTaskId()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/createtask");
            CreateTaskRequestModel body = new CreateTaskRequestModel()
            {
                TaskTitle = "New task " + RandomNumber.Next(),
                TaskDescription = "Description for new task",
                EmailOwner = NewUserEmail(),
                EmailAssign = NewUserEmail()
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            return int.Parse(jsonResponse["id_task"].ToString());
        }

        public int NewCompanyId(int companyNumber)
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/createcompany");
            CreateCompanyRequestModel body = new CreateCompanyRequestModel()
            {
                CompanyName = "Alcoholics and Parasites " + RandomNumber.Next(),
                CompanyType = "ООО",
                CompanyUsers = new List<string> { NewUserEmail(), NewUserEmail() },
                EmailOwner = NewUserEmail()
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            return int.Parse(jsonResponse["id_company"].ToString());
        }
    }
}
