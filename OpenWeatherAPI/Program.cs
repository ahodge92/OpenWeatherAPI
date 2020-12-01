using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace OpenWeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please Enter Zip Code:");
            var zip = int.Parse(Console.ReadLine());
            var temp = WeatherByZip(zip);
            Console.WriteLine($"The temperature is {temp} degress F outside.");
        }

        public static double WeatherByZip(int zipCode)
        {
            var weatherClient = new HttpClient();


            string connectionKey = File.ReadAllText("apikey.json");
            JObject jsonObject = JObject.Parse(connectionKey);
            JToken token = jsonObject["OpenWeatherApiKey"];
            string connString = token.ToString();

            var weather = $"http://api.openweathermap.org/data/2.5/weather?zip={zipCode}&appid={connString}";
            var weatherReponse = weatherClient.GetStringAsync(weather).Result;
            var kelvin = double.Parse(JObject.Parse(weatherReponse)["main"]["temp"].ToString());

            return KelvinToFah(kelvin);
        }

        public static double KelvinToFah(double kelvin)
        {
            return (kelvin - 273.15) * (9 / 5) + 32; 
        }
    }
}
