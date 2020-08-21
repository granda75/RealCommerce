using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.DAL
{
    public class CurrentWeather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string WeatherText { get; set; }
        public double Temperature { get; set; }

        public bool HasPrecipitation { get; set; }

    }
}
