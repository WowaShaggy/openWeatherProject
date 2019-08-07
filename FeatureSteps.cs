using Microsoft.VisualStudio.TestTools.UnitTesting;
using openweatherApiProject;
using System;
using System.Collections.Specialized;
using TechTalk.SpecFlow;

namespace bddSpec
{
    [Binding]
    public class FeatureSteps : LoggerClass
    {
        RestApiHelper<Parameters> restApi;
        NameValueCollection paramCollection;
        Parameters content;
        static string appid = "31362b85f4a911192388e8512299ae37";
        string operationName;
        string id;

        [Before("stations")]
        public void BeforeScenario() {
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

        [When(@"I gonna get ""(.*)""")]
        public void WhenIGonnaGet(string p0)
        {
            restApi = new RestApiHelper<Parameters>();
            paramCollection = new NameValueCollection();
            operationName = p0;
        }
        
        [When(@"I add ""(.*)"" with value ""(.*)""")]
        public void WhenIAddWithValue(string p0, string p1)
        {
            paramCollection.Add(p0, p1);
        }
        
        [When(@"I add my AppId")]
        public void WhenIAddMyAppId()
        {
            paramCollection.Add("APPID", appid);
        }
        
        [When(@"I send GET request")]
        public void WhenISendGETRequest()
        {
            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, paramCollection));
            var restRequest = restApi.CreateGetRequest();
            var response = restApi.GetResponse(restUrl, restRequest);
            content = restApi.GetContent<Parameters>(response);
        }

        [When(@"I send GET request with id")]
        public void WhenISendGETRequestWithId()
        {
            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, id, paramCollection));
            var restRequest = restApi.CreateGetRequest();
            var response = restApi.GetResponse(restUrl, restRequest);
            content = restApi.GetContent<Parameters>(response);
        }

        [Then(@"Cod should be equal (.*)")]
        public void ThenCodShouldBeEqual(int p0)
        {
            Assert.AreEqual(p0, content.cod);
        }
        
        [Then(@"Id should be equal (.*)")]
        public void ThenIdShouldBeEqual(int p0)
        {
            Assert.AreEqual(p0, content.city.id);
        }
        
        [Then(@"City name should be equal ""(.*)""")]
        public void ThenCityNameShouldBeEqual(string p0)
        {
            Assert.AreEqual(p0, content.city.name);
        }

        [Then(@"Name should be equal ""(.*)""")]
        public void ThenNameShouldBeEqual(string p0)
        {
            Assert.AreEqual(p0, content.name);
        }

        [Then(@"MainTemp should not be equal empty")]
        public void ThenMainTempShouldNotBeEqualEmpty()
        {
            Assert.IsNotNull(content.main.temp);
        }


        [When(@"I gonna get station ""(.*)"" request")]
        public void WhenIGonnaGetStationRequest(string p0)
        {
            restApi = new RestApiHelper<Parameters>(p0);
            operationName = "stations";
            paramCollection = new NameValueCollection();
        }

        [When(@"I add id ""(.*)""")]
        public void WhenIAddId(string p0)
        {
            id = p0;
        }


        [When(@"Name should be equal ""(.*)""")]
        public void WhenNameShouldBeEqual(string p0)
        {
            Assert.AreEqual(p0, content.name);
        }

        [When(@"Longitude should be equal (.*)")]
        public void WhenLongitudeShouldBeEqual(Double p0)
        {
            Assert.AreEqual(p0, content.longitude);
        }

        [When(@"Latitude should be equal (.*)")]
        public void WhenLatitudeShouldBeEqual(Double p0)
        {
            Assert.AreEqual(p0, content.latitude);
        }

        [When(@"Altitude should be equal (.*)")]
        public void WhenAltitudeShouldBeEqual(int p0)
        {
            Assert.AreEqual(p0, content.altitude);
        }

        [Then(@"I send PUT request")]
        public void ThenISendPUTRequest()
        {
            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, id, paramCollection));
            string jsonRequest = @"{
                                    ""external_id"": ""SF_UPD001"",
                                    ""name"": ""Wowa-City"",
                                    ""latitude"": 20.20,
                                    ""longitude"": -20.20,
                                    ""altitude"": 20
                                  }";
            var restRequest = restApi.CreatePutRequest(jsonRequest);
            var response = restApi.GetResponse(restUrl, restRequest);
            content = restApi.GetContent<Parameters>(response);
        }

        [Then(@"Name should not be equal ""(.*)""")]
        public void ThenNameShouldNotBeEqual(string p0)
        {
            Assert.AreNotEqual(p0, content.name);
        }

        [Then(@"Longitude should not be equal (.*)")]
        public void ThenLongitudeShouldNotBeEqual(Decimal p0)
        {
            Assert.AreNotEqual(p0, content.longitude);
        }

        [Then(@"Latitude should not be equal (.*)")]
        public void ThenLatitudeShouldNotBeEqual(Decimal p0)
        {
            Assert.AreNotEqual(p0, content.latitude);
        }

        [Then(@"Altitude should not be equal (.*)")]
        public void ThenAltitudeShouldNotBeEqual(int p0)
        {
            Assert.AreNotEqual(p0, content.altitude);
        }

    }
}
