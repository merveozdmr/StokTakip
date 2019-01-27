namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Raf")]
    public partial class Raf
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Raf()
        {
            Stok = new HashSet<Stok>();
        }

        public int RafID { get; set; }

        public int? DepoID { get; set; }

        [StringLength(50)]
        public string RafAdi { get; set; }

        public virtual Depo Depo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stok> Stok { get; set; }
    }
}
