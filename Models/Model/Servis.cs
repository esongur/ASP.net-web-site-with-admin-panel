using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Servis")]
    public class Servis
    {
        [Key]
        public int ServisId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string ResimUrl { get; set; }
    }
}