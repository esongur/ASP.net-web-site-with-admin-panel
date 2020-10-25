using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Urunler")]
    public class Urunler
    {
        [Key]
        public int UrunlerId { get; set; }
        [DisplayName("Urun Adı")]
        [Required,StringLength(100,ErrorMessage ="100 Karakter Olmalı...")]
        public string UrunlerBaslik { get; set; }
        [DisplayName("Urun Açıklama")]
        [Required, StringLength(400, ErrorMessage = "400 Karakter Olmalı...")]
        public string UrünlerAciklama { get; set; }
        public string UrunlerFiyat { get; set; }
        public string UrunlerResimURL1 { get; set; }
        public string UrunlerResimURL2{ get; set; }
        public string UrunlerResimURL3 { get; set; }
        public int UrunKategoriId { get; set; }
        public UrunKategori UrunKategori { get; set; }

    }
}