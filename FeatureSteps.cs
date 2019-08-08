using Microsoft.VisualStudio.TestTools.UnitTesting;
using openweatherApiProject;
using System;
using System.Collections.Specialized;
using System.Net;
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
        string newStationID;
        HttpStatusCode responseCode;


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
        [Given(@"I add my AppId")]
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
        [Given(@"I send GET request with id")]
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
        [Given(@"Name should be equal ""(.*)""")]
        public void ThenNameShouldBeEqual(string p0)
        {
            Assert.AreEqual(p0, content.name);
        }

        [Then(@"MainTemp should not be equal empty")]
        public void ThenMainTempShouldNotBeEqualEmpty()
        {
            Assert.IsNotNull(content.main.temp);
        }


        [Given(@"I gonna get station ""(.*)"" request")]
        public void WhenIGonnaGetStationRequest(string p0)
        {
            restApi = new RestApiHelper<Parameters>(p0);
            operationName = "stations";
            paramCollection = new NameValueCollection();
        }

        [Given(@"I add id ""(.*)""")]
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
        [Given(@"Longitude should be equal (.*)")]
        public void WhenLongitudeShouldBeEqual(Double p0)
        {
            Assert.AreEqual(p0, content.longitude);
        }

        [When(@"Latitude should be equal (.*)")]
        [Given(@"Latitude should be equal (.*)")]
        public void WhenLatitudeShouldBeEqual(Double p0)
        {
            Assert.AreEqual(p0, content.latitude);
        }

        [When(@"Altitude should be equal (.*)")]
        [Given(@"Altitude should be equal (.*)")]
        public void WhenAltitudeShouldBeEqual(int p0)
        {
            Assert.AreEqual(p0, content.altitude);
        }

        [Then(@"I send PUT request")]
        [When(@"I send PUT request")]
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

        [Given(@"I send POST request")]
        public void GivenISendPOSTRequest()
        {
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
            content = restApi.GetContent<Parameters>(response);
        }

        [Given(@"Id should not be empty")]
        public void GivenIdShouldNotBeEmpty()
        {
            Assert.IsNotNull(content.ID);
            newStationID = content.ID;
        }

        [When(@"I send DELETE request by ID")]
        public void WhenISendDELETERequestByID()
        {
            var restUrl = restApi.SetUrl(restApi.QueryBuilder(operationName, newStationID, paramCollection));
            var restRequest = restApi.CreateDeleteRequest();
            var response = restApi.GetResponse(restUrl, restRequest);
            responseCode = response.StatusCode;
            content = restApi.GetContent<Parameters>(response);
        }

        [Then(@"Status codeare is NoContent")]
        public void ThenStatusCodeareIsNoContent()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, responseCode);
        }

        [Then(@"Content is empty")]
        public void ThenContentIsEmpty()
        {
            Assert.IsNull(content);
        }

    }
}
