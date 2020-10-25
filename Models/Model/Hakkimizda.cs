using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int HakkimizdaId { get; set; }
        [Required,MaxLength]
        [DisplayName(" Hakkımızda Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName(" Hakkımızda Resim 1")]
        public string ResimUrl1 { get; set; }
        [DisplayName(" Hakkımızda Resim 2")]
        public string ResimUrl2 { get; set; }
        [DisplayName(" Hakkımızda Resim 3")]
        public string ResimUrl3 { get; set; }
    }
}