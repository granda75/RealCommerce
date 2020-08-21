using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealCommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Weather.BL;
using Weather.DAL;

namespace RealCommerce.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeatherController : ApiController
    {
        #region Fields

        private WeatherBL _weatherBl;
        private string _filePath = System.Web.Hosting.HostingEnvironment.MapPath("/AutoCompleteResponse.json");

        #endregion


        #region Constructors

        public WeatherController()
        {
            _weatherBl = new WeatherBL();
        }

        #endregion

        [HttpGet]
        public async Task<List<AutoCompleteCity>> Search(string cityName)
        {
            string searchApiUrl = ConfigurationManager.AppSettings["AutoCompleteUrl"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];
            string apiLanguage = ConfigurationManager.AppSettings["ApiLanguage"];

            List<AutoCompleteCity> cityItemsList = null;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
          
            string searchUrlWithParam = searchApiUrl + "?apikey=" + apiKey + "&q=" + cityName;

            HttpResponseMessage response = await client.GetAsync(searchUrlWithParam);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                cityItemsList  = JsonConvert.DeserializeObject<List<AutoCompleteCity>>(jsonString);
            }
            else
            {
                string jsonString = System.IO.File.ReadAllText(_filePath);
                cityItemsList = JsonConvert.DeserializeObject<List<AutoCompleteCity>>(jsonString);
            }    
            return cityItemsList;
            
        }

        [HttpGet]
        public async Task<List<CurrentWeather>> GetCurrentWeather(string cityKey)
        {
            List<CurrentConditions> weatherConditions = null;
            List<CurrentWeather> currentWeatherLst = _weatherBl.GetCurrentWeather(cityKey);
            if ( currentWeatherLst != null && currentWeatherLst.Count > 0 )
            {
                return currentWeatherLst;
            }

            string conditionsApiUrl = ConfigurationManager.AppSettings["CurrentConditionsUrl"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];
            string apiLanguage = ConfigurationManager.AppSettings["ApiLanguage"];
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");

            string searchUrlWithParams = conditionsApiUrl + cityKey + "?apikey=" + apiKey + "&details=false";
            HttpResponseMessage response = await client.GetAsync(searchUrlWithParams);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                weatherConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(jsonString);
                if ( weatherConditions != null && weatherConditions.Count > 0 )
                {
                    currentWeatherLst = new List<CurrentWeather>();
                    foreach (CurrentConditions condition in weatherConditions)
                    {
                        if ( condition != null )
                        {
                            CurrentWeather weather = new CurrentWeather();
                            weather.HasPrecipitation = condition.HasPrecipitation;
                            weather.WeatherText = condition.WeatherText;
                            weather.CityKey = cityKey;
                            weather.CurrentDate = DateTime.Now.Date;
                            if (condition.Temperature != null && condition.Temperature.Metric != null)
                            { 
                                weather.Temperature = (decimal)condition.Temperature.Metric.Value; 
                            }
                            currentWeatherLst.Add(weather);
                        }
                    }
                }
            }
            else
            {
                CurrentWeather weather = new CurrentWeather();
                weather.CityKey = cityKey;
                weather.Temperature = 32.5m;
                weather.HasPrecipitation = false;
                weather.WeatherText = "Clear";
                weather.CurrentDate = DateTime.Now.Date;
                currentWeatherLst.Add(weather);
            }

            if (currentWeatherLst != null && currentWeatherLst.Count > 0)
            {
                foreach (CurrentWeather weather in currentWeatherLst)
                {
                    _weatherBl.AddCurrentWeather(weather);
                }
            }

            return currentWeatherLst;
        }

        [HttpGet]
        public List<FavoriteCity> GetFavorites()
        {
            return _weatherBl.GetFavorites();
        }

        [HttpPost]
        public Response AddToFavorites([FromBody] FavoriteCity city)
        {
            return _weatherBl.AddFavorite(city);
        }

        [HttpDelete]
        public bool DeleteFavorite(string cityId)
        {
            return _weatherBl.DeleteFavorite(cityId);
        }

    }
}
