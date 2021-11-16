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
    class DeleteUserTests
    {
        [SetUp]
        public void Setup()
        {
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
