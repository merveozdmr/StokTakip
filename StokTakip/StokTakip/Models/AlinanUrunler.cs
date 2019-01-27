namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlinanUrunler")]
    public partial class AlinanUrunler
    {
        public int ID { get; set; }

        public int? StokID { get; set; }

        public int? AlinanUrunSayisi { get; set; }

        public virtual Stok Stok { get; set; }
    }
}
