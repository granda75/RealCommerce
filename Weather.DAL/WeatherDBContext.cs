namespace Weather.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WeatherDBContext : DbContext
    {
        public WeatherDBContext()
            : base("name=WeatherDBContext")
        {
        }

        public virtual DbSet<CurrentWeather> CurrentWeather { get; set; }

        public virtual DbSet<FavoriteCity> FavoriteCity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentWeather>()
                .Property(e => e.WeatherText)
                .IsUnicode(false);

            modelBuilder.Entity<CurrentWeather>()
                .Property(e => e.Temperature)
                .HasPrecision(3, 1);

            modelBuilder.Entity<CurrentWeather>()
                .Property(e => e.CityKey)
                .IsUnicode(false);
        }
    }
}
