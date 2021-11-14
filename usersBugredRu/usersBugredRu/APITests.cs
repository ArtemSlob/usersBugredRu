using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace usersBugredRu
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public void RegistrationTest()
        {
            RestClient client = new RestClient("http://users.bugred.ru/tasks/rest/doregister")
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");

            Dictionary<string, string> body = GenerateUserData();
            request.AddJsonBody(body);

            IRestResponse response = client.Execute(request);

            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body["email"], json["account"]["email"]?.ToString());
        }

        public Dictionary<string, string> GenerateUserData()
        {
            string email = "milli1@gmail.ru";
            string name = "Mashenka";   
            string password = "1";
            return new Dictionary<string, string>()
            {
                {"email", email },
                {"name", name },
                {"password", password }
            };
        }
    }
}