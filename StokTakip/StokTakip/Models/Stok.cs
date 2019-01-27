namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stok")]
    public partial class Stok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stok()
        {
            AlinanUrunler = new HashSet<AlinanUrunler>();
            SilinmisUrunler = new HashSet<SilinmisUrunler>();
        }

        public int StokID { get; set; }

        public int UrunID { get; set; }

        public int? StokAdet { get; set; }

        public int? KritikStokAdedi { get; set; }

        [StringLength(50)]
        public string StokKayitYapan { get; set; }

        public int? RafID { get; set; }

        public int? BirimID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlinanUrunler> AlinanUrunler { get; set; }

        public virtual Birim Birim { get; set; }

        public virtual Raf Raf { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SilinmisUrunler> SilinmisUrunler { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
