using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.DAL
{
    public class WeatherRepository
    {
        #region Fields

        private WeatherDBContext _context;

        #endregion

        #region Constructor

        public WeatherRepository()
        {

        }

        #endregion

        #region Public methods

        public List<FavoriteCity> GetFavorites()
        {
            using (_context = new WeatherDBContext())
            {
                return _context.FavoriteCity.ToList();
            }
        }

        public List<CurrentWeather> GetCityCurrentWeather(string cityKey)
        {
            List<CurrentWeather> currentWeatherList = null;
            using (_context = new WeatherDBContext())
            {
                 currentWeatherList = _context.CurrentWeather.ToList();
                 currentWeatherList = currentWeatherList.Where(l => l.CityKey == cityKey && l.CurrentDate == DateTime.Now.Date).Select(l => l).ToList();
            }
            return currentWeatherList;
        }

        public void AddCurrentWeather(CurrentWeather weather)
        {
            using (_context = new WeatherDBContext())
            {
                _context.CurrentWeather.Add(weather);
                _context.SaveChanges();
            }
        }

        public Response AddFavorite(FavoriteCity city)
        {
            Response response = new Response();
            using (_context = new WeatherDBContext())
            {
                FavoriteCity favorite =_context.FavoriteCity.Where(l => l.Key == city.Key).Select(m => m).FirstOrDefault();
                if ( favorite == null )
                {
                    _context.FavoriteCity.Add(city);
                    _context.SaveChanges();

                    response.ErrorCode = 0;
                    response.Message = "Favorite city was added successfully!";
                }
                else
                {
                    response.ErrorCode = 1;
                    response.Message = "The favorite city already exists in the database !";
                }
            }

            return response;
        }

        public bool DeleteFavorite(FavoriteCity city)
        {
            using (_context = new WeatherDBContext())
            {
                FavoriteCity favorite = _context.FavoriteCity.Find(city.Id);
                if (favorite != null)
                {
                    _context.FavoriteCity.Remove(favorite);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        #endregion
    }
}
