using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Models
{
    public class WeatherData
    {
        public Location Location { get; set; }
        public CurrentWeather Current { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }    
        public string Localtime { get; set; }  // Локальное время
        public string Region { get; set; }  // Локальное время
        public string Country { get; set; }  // Локальное время
    }

    public class CurrentWeather
    {
        public decimal Temp_C { get; set; }      // Температура в градусах Цельсия
        public Condition Condition { get; set; }
        public decimal Wind_Kph { get; set; }    // Скорость ветра в км/ч
    }

    public class Condition
    {
        public string Text { get; set; }       // Описание погоды
    }
}
