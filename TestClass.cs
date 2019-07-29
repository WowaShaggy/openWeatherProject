using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Newtonsoft.Json;
using NLog;

namespace openweatherApiProject
{
    [TestClass]
    public class TestClass : LoggerClass
    {
        static string appid = "31362b85f4a911192388e8512299ae37";

        [TestMethod]
        public void RestSharpTestForecast()
        {
            RestApiHelper<Parameters> restApi = new RestApiHelper<Parameters>();
            NameValueCollection paramCollection = new NameValueCollection();
            string operationName = "forecast";
            paramCollection.Add("id", "524901");
            paramCollection.Add("APPID", appid);
      
            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, paramCollection));
            var restRequest = restApi.CreateGetRequest();
            var response = restApi.GetResponse(restUrl, restRequest);
            Parameters content = restApi.GetContent<Parameters>(response);
            Assert.AreEqual(200, content.cod);
            Assert.AreEqual(524901, content.city.id);
            Assert.AreEqual("Moscow", content.city.name);
        }
      
       [TestMethod]
       public void RestSharpTestWeather()
       {
           RestApiHelper<Parameters> restApi = new RestApiHelper<Parameters>();
           NameValueCollection paramCollection = new NameValueCollection();
           string operationName = "weather";
           paramCollection.Add("q", "London");
           paramCollection.Add("APPID", appid);
      
           var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, paramCollection));
           var restRequest = restApi.CreateGetRequest();
           var response = restApi.GetResponse(restUrl, restRequest);
           Parameters content = restApi.GetContent<Parameters>(response);
           Assert.AreEqual(200, content.cod);
           Assert.AreEqual("London", content.name);
           Assert.IsNotNull(content.main.temp);
       }

        [TestMethod]
        public void RestSharpTestStationsPut()
        {
            RestApiHelper<Parameters> restApi = new RestApiHelper<Parameters>("3.0");
            string operationName = "stations";
            string id = "5d3afec76c634e000131c036";
            NameValueCollection paramCollection = new NameValueCollection();
            paramCollection.Add("APPID", appid);

            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, id, paramCollection));
            var restRequest = restApi.CreateGetRequest();
            var response = restApi.GetResponse(restUrl, restRequest);
            Parameters content = restApi.GetContent<Parameters>(response);
            Assert.AreEqual("San Francisco Test Station", content.name); string nameValueBefore = content.name;
            Assert.AreEqual(-122.43, content.longitude); double longitudeValueBefore = content.longitude;
            Assert.AreEqual(37.76, content.latitude); double latitudeValueBefore = content.latitude;
            Assert.AreEqual(150, content.altitude); int altitudeValueBefore = content.altitude;


            string jsonRequest = @"{
                                    ""external_id"": ""SF_UPD001"",
                                    ""name"": ""Wowa-City"",
                                    ""latitude"": 20.20,
                                    ""longitude"": -20.20,
                                    ""altitude"": 20
                                  }";


            restRequest = restApi.CreatePutRequest(jsonRequest);
            response = restApi.GetResponse(restUrl, restRequest);
            content = restApi.GetContent<Parameters>(response);
            Assert.AreNotEqual(nameValueBefore, content.name); 
            Assert.AreNotEqual(longitudeValueBefore, content.longitude); 
            Assert.AreNotEqual(latitudeValueBefore, content.latitude); 
            Assert.AreNotEqual(altitudeValueBefore, content.altitude);
        }

        [TestMethod]
        public void RestSharpTestStationsPostAndDelete()
        {
            RestApiHelper<Parameters> restApi = new RestApiHelper<Parameters>("3.0");
            string operationName = "stations";
            NameValueCollection paramCollection = new NameValueCollection{{ "APPID", appid }};
            string jsonRequest = @"{
                                     ""external_id"": ""SF_TEST020"",
                                     ""name"": ""Brest"",
                                     ""longitude"": -122.43,
                                     ""latitude"": 37.76,
                                     ""altitude"": 150
                                   }";

            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, paramCollection));
            var restRequest = restApi.CreatePostRequest(jsonRequest);
            var response = restApi.GetResponse(restUrl, restRequest);
            Parameters content = restApi.GetContent<Parameters>(response);
            Assert.AreEqual("Brest", content.name);
            Assert.AreEqual(150, content.altitude);
            Assert.IsNotNull(content.ID);
            string newStationID = content.ID;

            restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, newStationID, paramCollection));
            restRequest = restApi.CreateDeleteRequest();
            response = restApi.GetResponse(restUrl, restRequest);
            content = restApi.GetContent<Parameters>(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.IsNull(content);
        }

       [TestMethod]
       public void HttpWebClientsAndStreamReaderTest()
       {
           string url = "http://api.openweathermap.org/data/2.5/forecast?id=524901&APPID=31362b85f4a911192388e8512299ae37";
           HttpWebRequest wrq = (HttpWebRequest)WebRequest.Create(url);
           HttpWebResponse wrs = (HttpWebResponse)wrq.GetResponse();
           string response;
           using (StreamReader sr = new StreamReader(wrs.GetResponseStream()))
           {
               response = sr.ReadToEnd();
           }
     
           WeatherStreamResponse wr = JsonConvert.DeserializeObject<WeatherStreamResponse>(response);
           Assert.AreEqual("200", wr.cod);
           Assert.AreEqual(40, wr.cnt);
           Assert.AreEqual(524901, wr.city.id);
       }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            RestApiHelper<Parameters> restApi = new RestApiHelper<Parameters>("3.0");
            string operationName = "stations";
            string id = "5d3afec76c634e000131c036";
            NameValueCollection paramCollection = new NameValueCollection();
            paramCollection.Add("APPID", appid);

            string jsonRequest = @"{
                                    ""external_id"": ""SF_TEST001"",
                                    ""name"": ""San Francisco Test Station"",
                                    ""longitude"": -122.43,
                                    ""latitude"": 37.76,
                                    ""altitude"": 150
                                  }";

            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, id, paramCollection));
            var restRequest = restApi.CreatePutRequest(jsonRequest);
            restApi.GetResponse(restUrl, restRequest);

        }

    }

}
