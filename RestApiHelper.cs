using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace openweatherApiProject
{
    public class RestApiHelper<T>
    {
        public RestClient _restClient;
        public RestRequest _restRequest;
        public string apiKey = "31362b85f4a911192388e8512299ae37";
        string _baseUrl = "http://api.openweathermap.org/data/";

        public RestApiHelper(string version = "2.5") {
            string baseur = version + "/";
            _baseUrl += baseur;
        }

        public RestClient SetUrl(string anotherUrl)
        {
            var url = Path.Combine(_baseUrl, anotherUrl);
            var _restClient = new RestClient(url);
            return _restClient;
        }

        public RestClient SetVersion(string anotherUrl)
        {
            var url = Path.Combine(_baseUrl, anotherUrl);
            var _restClient = new RestClient(url);
            return _restClient;
        }

        public RestRequest CreatePostRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.POST);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public RestRequest CreateGetRequest()
        {
            _restRequest = new RestRequest(Method.GET);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }

        public RestRequest CreateDeleteRequest()
        {
            _restRequest = new RestRequest(Method.DELETE);
            _restRequest.AddHeader("Accept", "application/json");
            return _restRequest;
        }

        public RestRequest CreatePutRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PUT);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO deseiralizeObject = JsonConvert.DeserializeObject<DTO>(content);
            return deseiralizeObject;
        }

        public string QueryBuilder(string operationname, NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return operationname + "?" + string.Join("&", array);
        }

        public string QueryBuilder(string operationname, string notIndicatedValue, NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return operationname + "/" + notIndicatedValue + "?" + string.Join("&", array);
        }

    }
}