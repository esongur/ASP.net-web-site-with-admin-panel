using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Sertifikalar")]
    public class Sertifikalar
    {
        [Key]
        public int SertifikaId { get; set; }
        public string Ad { get; set; }
        public string ResimURL { get; set; }
    }
}