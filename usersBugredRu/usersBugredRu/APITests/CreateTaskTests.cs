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
    class CreateTaskTests
    {
        private RequestHelper _requestHelper;
        private Helper _helper;

        [SetUp]
        public void Setup()
        {
            _requestHelper = new RequestHelper("tasks/rest/createtask");
            _helper = new Helper();
        }

        [Test]
        public void CreateTaskTest()
        {
            CreateTaskRequestModel body = new CreateTaskRequestModel()
            {
                TaskTitle = "New task",
                TaskDescription = "Description for new task",
                EmailOwner = _helper.NewUserEmail(),
                EmailAssign = _helper.NewUserEmail()
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("success", jsonResponse["type"].ToString());
            Assert.AreEqual("Задача успешно создана!", jsonResponse["message"].ToString());
            Assert.IsTrue(int.TryParse(jsonResponse["id_task"].ToString(), out int taskId));
        }
    }
}
