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
        private RequestHelper _requestHelper;
        private Helper _helper;

        [SetUp]
        public void Setup()
        {
            _requestHelper = new RequestHelper("tasks/rest/deleteuser");
            _helper = new Helper();
        }

        [Test]
        public void DeleteUserTest()
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "email", _helper.NewUserEmail()}
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Пользователь с email " + body["email"] + " успешно удален", jsonResponse["message"].ToString());
        }
    }
}
