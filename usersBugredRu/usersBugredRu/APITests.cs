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
            RequestHelper requestHelper = new RequestHelper("doregister");
            Helper helper = new Helper();
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = "mashenka" + helper.DateTimeNowString + "@gmail.com",
                Name = "Mashenka",
                Password = "1"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            //Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
        }

        [Test]
        public void CreateCompanyTest()
        {
            RequestHelper requestHelper = new RequestHelper("createcompany");
            Helper helper = new Helper();
            CreateCompanyRequestModel body = new CreateCompanyRequestModel()
            {
                CompanyName = "Alcoholics and Parasites",
                CompanyType = "ннн",
                CompanyUsers = new List<string> { "mdiakonova_967@test.com", "julia2@gmail.com" },
                EmailOwner = "mashenka@gmail.com"
            };
            IRestResponse response = requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);
            //Console.WriteLine(jsonResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.CompanyName, jsonResponse["company"]["name"].ToString());
            Assert.AreEqual(body.CompanyType, jsonResponse["company"]["type"].ToString());            
            Assert.IsTrue(helper.CheckResponseForAllCompanyUsers(body.CompanyUsers, jsonResponse["company"]["users"].ToString()));
        }
    }
}