using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DAL;

namespace Weather.BL
{
    public class WeatherBL
    {
        #region Fields

        private WeatherRepository _repo;

        #endregion

        #region Constructor

        public WeatherBL()
        {
            _repo = new WeatherRepository();
        }

        #endregion

        #region Public methods

        public List<FavoriteCity> GetFavorites()
        {
            return _repo.GetFavorites();
        }

        public void AddCurrentWeather(CurrentWeather weather)
        {
             _repo.AddCurrentWeather(weather);
        }

        public List<CurrentWeather> GetCurrentWeather(string cityKey)
        {
            return _repo.GetCityCurrentWeather(cityKey);
        }

        public Response AddFavorite(FavoriteCity city)
        {
            return _repo.AddFavorite(city);
        }

        public bool DeleteFavorite(string cityId)
        {
            FavoriteCity city = new FavoriteCity();
            city.Id = Convert.ToInt32(cityId);
            return _repo.DeleteFavorite(city);
        }

        #endregion
    }
}
