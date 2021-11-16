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
        private RequestHelper _requestHelper;
        private Helper _helper;

        [SetUp]
        public void Setup()
        {
            _requestHelper = new RequestHelper("tasks/rest/createcompany");
            _helper = new Helper();
        }

        [Test]
        public void CreateCompanyTest()
        {
            CreateCompanyRequestModel body = new CreateCompanyRequestModel()
            {
                CompanyName = "Alcoholics and Parasites",
                CompanyType = "ООО",
                CompanyUsers = new List<string> { _helper.NewUserEmail(), _helper.NewUserEmail() },
                EmailOwner = _helper.NewUserEmail()
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.CompanyName, jsonResponse["company"]["name"].ToString());
            Assert.AreEqual(body.CompanyType, jsonResponse["company"]["type"].ToString());
            Assert.IsTrue(_helper.CheckResponseForAllCompanyUsers(body.CompanyUsers, jsonResponse["company"]["users"].ToString()));
        }       
    }
}
