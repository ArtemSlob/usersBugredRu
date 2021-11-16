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
                CompanyUsers = new List<string> { helper.NewUserEmail(), helper.NewUserEmail() },
                EmailOwner = helper.NewUserEmail()
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
                TaskTitle = "New task",
                TaskDescription = "Description for new task",
                EmailOwner = helper.NewUserEmail(),
                EmailAssign = helper.NewUserEmail()
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
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
            CreateUserRequestModel body = new CreateUserRequestModel()
            {
                Email = "petya" + helper.DateTimeNowString + "@gmail.com",
                Name = "Petya" + helper.DateTimeNowString,
                Tasks = new List<int> { helper.NewTaskId(), helper.NewTaskId(), helper.NewTaskId() },
                Companies = new List<int> { helper.NewCompanyId(1), helper.NewCompanyId(2) }
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
            Assert.AreEqual(body.Name, jsonResponse["name"].ToString());
            Assert.AreEqual(dateNow, jsonResponse["date"].ToString());
            Assert.IsTrue(helper.CheckResponseForIntDataFromRequest(body.Tasks, jsonResponse["tasks"].ToString()));
            Assert.IsTrue(helper.CheckResponseForIntDataFromRequest(body.Companies, jsonResponse["companies"].ToString()));
        }

        [Test]
        public void DeleteUserTest()
        {
            RequestHelper requestHelper = new RequestHelper("tasks/rest/deleteuser");
            Helper helper = new Helper();
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "email", helper.NewUserEmail()}
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Пользователь с email " + body["email"] + " успешно удален", jsonResponse["message"].ToString());
        }
    }
}