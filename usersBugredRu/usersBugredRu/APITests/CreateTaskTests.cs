﻿using Newtonsoft.Json.Linq;
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
        [SetUp]
        public void Setup()
        {
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
    }
}
