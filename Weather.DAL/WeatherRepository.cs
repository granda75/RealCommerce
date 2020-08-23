using System;
using System.Collections.Generic;
using System.Linq;


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

        /// <summary>
        /// Gets favorits cities from FavoriteCity table. 
        /// </summary>
        /// <returns></returns>
        public List<FavoriteCity> GetFavorites()
        {
            using (_context = new WeatherDBContext())
            {
                return _context.FavoriteCity.ToList();
            }
        }

        /// <summary>
        /// Gets current weather of the city.
        /// </summary>
        /// <param name="cityKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The method adds current weather to the CurrentWeather table.
        /// </summary>
        /// <param name="weather"></param>
        public void AddCurrentWeather(CurrentWeather weather)
        {
            using (_context = new WeatherDBContext())
            {
                _context.CurrentWeather.Add(weather);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// The method adds city to FavoriteCity table.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove the city from FavoriteCity table.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
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
