using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using usersBugredRu.Helpers;
using static usersBugredRu.Models.RegisterRequestModel;

namespace usersBugredRu
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RegistrationTest()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/doregister");
            Helper helper = new Helper();
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = "mashenka" + helper.DateTimeNowString + "@gmail.com",
                Name = "Mashenka" + helper.DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Name, jsonResponse["name"].ToString());
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
        }

        [Test]
        public void CreateCompanyTest()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/createcompany");
            Helper helper = new Helper();
            CreateCompanyRequestModel body = new CreateCompanyRequestModel()
            {
                CompanyName = "Alcoholics and Parasites",
                CompanyType = "ООО",
                CompanyUsers = new List<string> { "mashenka7@gmail.com", "mashenka9@gmail.com" },
                EmailOwner = "mashenka@gmail.com"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.CompanyName, jsonResponse["company"]["name"].ToString());
            Assert.AreEqual(body.CompanyType, jsonResponse["company"]["type"].ToString());            
            Assert.IsTrue(helper.CheckResponseForAllCompanyUsers(body.CompanyUsers, jsonResponse["company"]["users"].ToString()));
        }

        [Test]
        public void CreateTaskTest()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/createtask");
            Helper helper = new Helper();
            CreateTaskRequestModel body = new CreateTaskRequestModel()
            {
                TaskTitle = "New task" + helper.DateTimeNowString,
                TaskDescription = "Description for new task" + helper.DateTimeNowString,
                EmailOwner = "mashenka@gmail.com",
                EmailAssign = "mashenka7@gmail.com"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("success", jsonResponse["type"].ToString());
            Assert.AreEqual("Задача успешно создана!", jsonResponse["message"].ToString());
            Assert.IsTrue(int.TryParse(jsonResponse["id_task"].ToString(), out int taskId));
        }

        [Test]
        public void CreateUserTest()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/createuser");
            Helper helper = new Helper();
            CreateUserRequestModel body = new CreateUserRequestModel()
            {
                Email = "Alcoholics and Parasites",
                Name = "ООО",
                Tasks = new List<int> { 210, 212, 213 },
                Companies = new List<int> {  }
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            //Console.WriteLine(jsonResponse);

            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.AreEqual(body.CompanyName, jsonResponse["company"]["name"].ToString());
            //Assert.AreEqual(body.CompanyType, jsonResponse["company"]["type"].ToString());
            //Assert.IsTrue(helper.CheckResponseForAllCompanyUsers(body.CompanyUsers, jsonResponse["company"]["users"].ToString()));
        }
    }
}