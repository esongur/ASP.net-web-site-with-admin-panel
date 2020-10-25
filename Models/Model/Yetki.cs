using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Yetki")]
    public class Yetki
    {
        [Key]
        public int YetkiId { get; set; }
        public string YetkiAd { get; set; }
    }
}