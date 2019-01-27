namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunDurum")]
    public partial class UrunDurum
    {
        [Key]
        public int DurumID { get; set; }

        public int? UrunID { get; set; }

        [StringLength(50)]

        public string UrunDurumu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HareketTarihi { get; set; }

        [StringLength(50)]
        public string Aciklama { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
