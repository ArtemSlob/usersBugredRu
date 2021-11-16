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

        public RequestHelper(string requestURN)
        {
            _client = new RestClient("http://users.bugred.ru/" + requestURN);
        }

        public IRestResponse SendPostRequest(object body)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(JsonConvert.SerializeObject(body));

            return _client.Execute(request);
        }
    }
}
