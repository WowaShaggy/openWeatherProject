using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openweatherApiProject
{
    public class WeatherStreamResponse
    {
        public CityStreamInfo city { get; set; }

        public string cod { get; set; }
        public int cnt { get; set; }
    }

    public class CityStreamInfo
    {
        public int id { get; set; }
    }
}
