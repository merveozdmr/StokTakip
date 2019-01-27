namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            Stok = new HashSet<Stok>();
            UrunDurum = new HashSet<UrunDurum>();
        }

        public int UrunID { get; set; }

        [StringLength(50)]
        public string UrunAdi { get; set; }

        [StringLength(50)]
        public string Marka { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(10)]
        public string SeriNo { get; set; }

        [StringLength(50)]
        public string KayitYapan { get; set; }

        public int? AltKategoriID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UrunAlimTarihi { get; set; }

        public int? FirmaID { get; set; }
        public decimal? Fiyat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GarantiBitisTarihi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GarantiUyarıTarihi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LisansBitisTarihi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LisansUyarıTarihi { get; set; }

        public virtual AltKategori AltKategori { get; set; }

        public virtual Firma Firma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stok> Stok { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunDurum> UrunDurum { get; set; }
    }
}
