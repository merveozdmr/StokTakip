namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SilinmisUrunler")]
    public partial class SilinmisUrunler
    {
        public int ID { get; set; }

        public int? StokID { get; set; }

        public int? SilinmisUrunSayisi { get; set; }

        public virtual Stok Stok { get; set; }
    }
}
