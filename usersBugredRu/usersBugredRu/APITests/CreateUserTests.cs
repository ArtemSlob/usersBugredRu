using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using usersBugredRu.Helpers;
using static usersBugredRu.Models.RegisterRequestModel;

namespace usersBugredRu.APITests
{
    class CreateUserTests
    {
        [SetUp]
        public void Setup()
        {
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
    }
}
