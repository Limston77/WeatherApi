using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class WeatherSerevice
    {
        private static string _town = string.Empty;
        private static string _apiWeatherKey = "cbb080428f8843fbb93191640242707";

        private static string _weatherURL =>
            $"http://api.weatherapi.com/v1/current.json?key={_apiWeatherKey}&q={_town}&aqi=no&lang=ru";

        public static async Task<WeatherData> GetDailyWeather(string town, HttpClient httpClient)
        {
            // Обновление URL с учетом нового города
                _town = town;
                HttpResponseMessage response = await httpClient.GetAsync(_weatherURL);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

                    // Проверка, удалось ли распарсить данные
                    if (weatherData == null || weatherData.Location == null || weatherData.Current == null)
                    {
                        return null;
                    }
                    else
                    {
                        return weatherData;
                    }
                }
                else
                {
                    return null;
                }
        }
    }
}
