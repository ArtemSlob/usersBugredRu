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
    class CreateCompanyTests
    {
        [SetUp]
        public void Setup()
        {
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
    }
}
