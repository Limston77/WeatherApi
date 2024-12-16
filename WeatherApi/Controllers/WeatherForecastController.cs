using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private  readonly HttpClient _httpClient;
        public WeatherForecastController()
        {
            _httpClient =  new HttpClient();
        }

        
        

        [HttpGet("GetWeather/{city}")]
        [SwaggerOperation(
        Summary = "Вывод настроек отправителя",
        Description = "Вывод текущих настроек почты, с которой посылаются запросы")]
        public async Task<ActionResult> GetCurrentWeatherInTown([FromRoute] string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City name is required.");
            }

            try
            {
                // Предполагается, что WeatherService.GetDailyWeather возвращает результат асинхронно
                var result = await WeatherSerevice.GetDailyWeather(city, _httpClient);

                if (result == null)
                {
                    return NotFound($"Weather data for city '{city}' not found.");
                    
                }
                else
                {
                    return Ok(result);
                }
             
            }
            catch (Exception ex)
            {
                // Логирование ошибки (например, через ILogger)
                return StatusCode(500, $"An error occurred while retrieving weather data: {ex.Message}");
            }
        }

    }
}
