using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    public class Biomedikal
    {
        [Key]
        public int BiomedikalId { get; set; }
        [DisplayName("Biomedikal Adı")]
        [Required, StringLength(200, ErrorMessage = "200 Karakter Olmalıdır...")]
        public string Baslik { get; set; }
        public string ResimURL { get; set; }
        public int BiomedikalKategoriId { get; set; }
        public BiomedikalKategori BiomedikalKategori { get; set; }
    }
}