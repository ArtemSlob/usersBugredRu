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
    class RegistrationTests
    {
        private RequestHelper _requestHelper;
        private Helper _helper;

        [SetUp]
        public void Setup()
        {
            _requestHelper = new RequestHelper("tasks/rest/doregister");
            _helper = new Helper();
        }

        [Test]
        public void PositiveRegistrationTest()
        {
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "mashenka" + _helper.DateTimeNowString + "@gmail.com",
                Name = Helper.RandomNumber.Next() + "Mashenka" + _helper.DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.Name, jsonResponse["name"].ToString());
            Assert.AreEqual(body.Email, jsonResponse["email"].ToString());
        }

        [Test]
        public void RegistrationUserWithEmailThatAlreadyRegisteredTest()
        {
            RegistrationRequestModel preConditionBody = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "mashenka" + _helper.DateTimeNowString + "@gmail.com",
                Name = Helper.RandomNumber.Next() + "Mashenka" + _helper.DateTimeNowString,
                Password = "1"
            };
            _requestHelper.SendPostRequest(preConditionBody);
            RegistrationRequestModel testBody = new RegistrationRequestModel()
            {
                Email = preConditionBody.Email,
                Name = Helper.RandomNumber.Next() + "Masha" + _helper.DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(testBody);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual("email " + preConditionBody.Email + " уже есть в базе", jsonResponse["message"].ToString());
        }

        [Test]
        public void RegistrationUserWithNameThatAlreadyRegisteredTest()
        {
            RegistrationRequestModel preConditionBody = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "mashenka" + _helper.DateTimeNowString + "@gmail.com",
                Name = Helper.RandomNumber.Next() + "Mashenka" + _helper.DateTimeNowString,
                Password = "1"
            };
            _requestHelper.SendPostRequest(preConditionBody);
            RegistrationRequestModel testBody = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "masha" + _helper.DateTimeNowString + "@gmail.com",
                Name = preConditionBody.Name,
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(testBody);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual("Текущее ФИО " + preConditionBody.Name + " уже есть в базе", jsonResponse["message"].ToString());
        }

        [Test]
        public void RegistrationUserThatAlreadyRegisteredTest()
        {
            RegistrationRequestModel preConditionBody = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "mashenka" + _helper.DateTimeNowString + "@gmail.com",
                Name = Helper.RandomNumber.Next() + "Mashenka" + _helper.DateTimeNowString,
                Password = "1"
            };
            _requestHelper.SendPostRequest(preConditionBody);
            RegistrationRequestModel testBody = new RegistrationRequestModel()
            {
                Email = preConditionBody.Email,
                Name = preConditionBody.Name,
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(testBody);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual("пользователь с email " + preConditionBody.Email + " и именем " + preConditionBody.Name + " уже есть в базе"
                ,jsonResponse["message"].ToString());
        }

        [Test]
        public void RegistrationUserWithoutNameTest()
        {
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = Helper.RandomNumber.Next() + "mashenka" + _helper.DateTimeNowString + "@gmail.com",
                Name = "",
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(422, (int)response.StatusCode);
            Assert.AreEqual("поле фио является обязательным", jsonResponse["message"].ToString());
        }

        [Test]
        public void RegistrationUserWithoutEmailTest()
        {
            RegistrationRequestModel body = new RegistrationRequestModel()
            {
                Email = "",
                Name = Helper.RandomNumber.Next() + "Mashenka" + _helper.DateTimeNowString,
                Password = "1"
            };
            IRestResponse response = _requestHelper.SendPostRequest(body);
            JObject jsonResponse = JObject.Parse(response.Content);

            Assert.AreEqual(422, (int)response.StatusCode);
            Assert.AreEqual("поле email является обязательным", jsonResponse["message"].ToString());
        }
    }
}
