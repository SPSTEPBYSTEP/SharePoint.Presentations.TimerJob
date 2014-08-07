using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicTests
{
    [TestClass]
    public class WeatherTest
    {
        [TestMethod]
        public void TestWeatherAPI()
        {
            var data = BusinessLogic.YahooWeather.Instance.GetWeatherDataAsJSON("55864247", "c");
            Console.WriteLine(data);
        }
    }
}
