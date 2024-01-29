using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace Webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Send GET request to weather.com
            String url = "https://weather.com/en-GB/weather/today/l/UKXX0085:1:UK?Goto=Redirected";

            // create http client object and assign to httpClient
            var httpClient = new HttpClient(); 

            // GET request to fetch HTML from url and return the data as an object inside var html
            var html = httpClient.GetAsync(url).Result; 

            // htmlDocument object instantiated 
            var htmlDocument = new HtmlDocument();

            // htmlDocument loads html object inside 'html' variable as a string 
            htmlDocument.LoadHtml(html.Content.ReadAsStringAsync().Result);

            // GET TEMPERATURE
            // takes the html string inside htmlDocument and looks for specific class
            var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--MHmYY']");
            // gets specific word or integer from the above line of html with .Trim to remove whitespace
            var temperature = temperatureElement.InnerText.Trim();
            Console.WriteLine("Temperature: " + temperature);


            // GET CONDITIONS
            var conditionElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='CurrentConditions--phraseValue--mZC_p']");

            var condition = conditionElement.InnerText.Trim();

            Console.WriteLine("Conditions: " + condition);


            // GET LOCATION
            var cityElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='CurrentConditions--location--1YWj_']");

            var city = cityElement.InnerText.Trim();

            Console.WriteLine("City: " + city);
        }
    }
}