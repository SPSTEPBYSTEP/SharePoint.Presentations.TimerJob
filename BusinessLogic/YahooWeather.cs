using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class YahooWeather
    {
        private YahooWeather()
        {

        }
        #region Singleton Instance
        private static object lockO = new object();
        private static YahooWeather _instance = null;
        public static YahooWeather Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (lockO)
                    {
                        if (null == _instance)
                        {
                            _instance = new YahooWeather();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        const string weatherUrl = "http://query.yahooapis.com/v1/public/yql?format=json&q=select * from weather.forecast where woeid=\"{0}\" and u=\"{1}\"";
        
        public string GetWeatherDataAsJSON(string woeId, string temperatureUnit)
        {
            var apiUrl = string.Format(weatherUrl, woeId, temperatureUnit);
            return new System.Net.WebClient().DownloadString(apiUrl);
        }
    }
}
