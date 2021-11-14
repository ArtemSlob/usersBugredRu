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
            RequestHelper requestHelper = new RequestHelper("http://users.bugred.ru/tasks/rest/doregister");
            Helper helper = new Helper();
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = "Mashenka" + helper.DateTimeNowString + "@gmail.com",
                Name = "Mashenka" + helper.DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            //Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
        }
    }
}