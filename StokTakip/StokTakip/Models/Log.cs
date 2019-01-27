namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(50)]
        public string ControllerName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Tarih { get; set; }

        [StringLength(300)]
        public string Bilgi { get; set; }
    }
}
