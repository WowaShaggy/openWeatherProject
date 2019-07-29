using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openweatherApiProject
{
    public class Parameters
    {
        public int cod { get; set; }

        public int code { get; set; }

        public string external_id { get; set; }

        public string message { get; set; }

        public string ID { get; set; }
        public string id { get; set; }

        public string name { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public int altitude { get; set; }


        public CityInfo city { get; set; }
       public MainInfo main { get; set; }
    }

    public class CityInfo
    {
        public int id { get; set; }
        public string name { get; set; }
    }
   
    public class MainInfo
    {
        public float temp { get; set; }
        public int pressure { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class LocalParser {

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double latitude { get; set; }

        [JsonProperty(PropertyName = "altitude")]
        public int altitude { get; set; }
    }
}

