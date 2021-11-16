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
        private RequestHelper _requestHelper;
        private Helper _helper;

        [SetUp]
        public void Setup()
        {
            _requestHelper = new RequestHelper("tasks/rest/createuser");
            _helper = new Helper();
        }

        [Test]
        public void CreateUserTest()
        {
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
            CreateUserRequestModel body = new CreateUserRequestModel()
            {
                Email = "petya" + _helper.DateTimeNowString + "@gmail.com",
                Name = "Petya" + _helper.DateTimeNowString,
                Tasks = new List<int> { _helper.NewTaskId(), _helper.NewTaskId(), _helper.NewTaskId() },
                Companies = new List<int> { _helper.NewCompanyId(1), _helper.NewCompanyId(2) }
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
            Assert.AreEqual(body.Name, jsonResponse["name"].ToString());
            Assert.AreEqual(dateNow, jsonResponse["date"].ToString());
            Assert.IsTrue(_helper.CheckResponseForIntDataFromRequest(body.Tasks, jsonResponse["tasks"].ToString()));
            Assert.IsTrue(_helper.CheckResponseForIntDataFromRequest(body.Companies, jsonResponse["companies"].ToString()));
        }
    }
}
