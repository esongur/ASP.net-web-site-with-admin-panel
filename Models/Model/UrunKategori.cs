using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("UrunKategori")]
    public class UrunKategori
    {
        [Key]
        public int UrunKategoriId { get; set; }
        [Required, StringLength(200, ErrorMessage = "200 Karakter Olmalı...")]
        [DisplayName("Ürün Kategori Adı")]
        public string UrunKategoriAd { get; set; }
        [Required, MaxLength]
        [DisplayName("Ürün Kategori Açıklama")]
        public string UrunAciklama { get; set; }
        public ICollection<Urunler> Urunlers { get; set; }
    }
}