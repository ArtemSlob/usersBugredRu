using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usersBugredRu.Helpers
{
    public class RequestHelper
    {
        private RestClient _client;

        public RequestHelper(string requestURL)
        {
            _client = new RestClient(requestURL);
        }

        public IRestResponse SendPostRequest(object body)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(JsonConvert.SerializeObject(body));

            IRestResponse response = _client.Execute(request);
            return response;
        }
    }
}
