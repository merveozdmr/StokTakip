namespace StokTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personel")]
    public partial class Personel
    {
        public int PersonelID { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string PersonelAd { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string PersonelSoyad { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string TcNo { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string Cinsiyet { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public DateTime? DogumTarih { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string PersonelAdres { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string PersonelTelefon { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public DateTime? KayitTarih { get; set; }

       
        [StringLength(50)]
        [Required(ErrorMessage = "Bu alaný boþ geçemezsiniz")]
        public string Email { get; set; }
    }
}
