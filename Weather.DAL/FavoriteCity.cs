namespace Weather.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoriteCity")]
    public partial class FavoriteCity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [StringLength(50)]
        public string LocalizedName { get; set; }
    }
}
