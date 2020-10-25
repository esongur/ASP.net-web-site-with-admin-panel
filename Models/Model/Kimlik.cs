using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikId { get; set; }
        [DisplayName("Site Başlık")]
        [Required,StringLength(100,ErrorMessage ="100 Karakter olmalı...")]
        public string Title { get; set; }
        [DisplayName("Anahtar Kelimeler")]
        [Required]
        public string Keywords { get; set; }
        [DisplayName("Site Açıklama")]
        [Required, StringLength(500,ErrorMessage ="500 Karakter Olmalı...")]
        public string Description { get; set; }
        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }
        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }

    }
}