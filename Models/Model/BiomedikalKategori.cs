using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("BiomedikalKategori")]
    public class BiomedikalKategori
    {
        [Key]
        public int BiomedikalKategoriId { get; set; }
        [Required, StringLength(200, ErrorMessage = "200 Karakter Olmalı...")]
        [DisplayName("Biomedikal Kategori Adı")]
        public string BiomedikalKategoriAd { get; set; }
        public ICollection<Biomedikal> Biomedikals { get; set; }

    }
}