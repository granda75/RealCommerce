namespace Weather.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrentWeather")]
    public partial class CurrentWeather
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string WeatherText { get; set; }

        public decimal? Temperature { get; set; }

        public bool? HasPrecipitation { get; set; }

        [StringLength(50)]
        public string CityKey { get; set; }

        public DateTime CurrentDate { get; set; }
    }
}
