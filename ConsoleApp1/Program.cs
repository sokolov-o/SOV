using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestJSON();

            Console.WriteLine("Press Enter...");
            Console.ReadLine();

        }
        /// <summary>
        /// using System.Text.Json
        /// </summary>
        static void TestJSON()
        {
            List<WeatherForecast> wfs = new List<WeatherForecast>()
            {
                new WeatherForecast(){ Date = DateTime.Today.AddHours(-5), TemperatureCelsius = 10, Summary = "1 forecast" } ,
                new WeatherForecast(){ Date = DateTime.Today.AddHours(-4), TemperatureCelsius = 12, Summary = "2 forecast" } ,
                new WeatherForecast(){ Date = DateTime.Today.AddHours(-3), TemperatureCelsius = 13, Summary = "3 forecast" } ,
                new WeatherForecast(){ Date = DateTime.Today.AddHours(-2), TemperatureCelsius = 14, Summary = "4 forecast" } ,
                new WeatherForecast(){ Date = DateTime.Today.AddHours(-1), TemperatureCelsius = 15, Summary = "5 forecast" }
            };

            // Serialize to file
            string jsonString;
            jsonString = JsonSerializer.Serialize(wfs, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText("json1.json", jsonString);

            Console.WriteLine($"{jsonString}\n\rOk. Serialized {wfs.Count} items.");

            // Deserialize from file
            jsonString = null;
            jsonString = File.ReadAllText("json1.json");
            wfs = JsonSerializer.Deserialize<List<WeatherForecast>>(jsonString);

            Console.WriteLine($"Ok. Deserialized {wfs.Count} items.");
        }
        public class WeatherForecast
        {
            [JsonInclude]
            public DateTimeOffset Date { get; set; }
            [JsonInclude]
            public int TemperatureCelsius { get; set; }
            [JsonIgnore]
            public string Summary { get; set; }
        }
    }
}
