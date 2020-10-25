using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("İletisim")]
    public class Iletisim
    {
        [Key]
        public int IletisimId { get; set; }
        [Required, StringLength(300,ErrorMessage ="300 Karakter olmalıdır...")]
        public string Adres { get; set; }
        [Required, StringLength(300, ErrorMessage = "300 Karakter olmalıdır...")]
        public string Telefon  { get; set; }
        public string Fax { get; set; }
        [Required, StringLength(300, ErrorMessage = "300 Karakter olmalıdır...")]
        public string Email { get; set; }
        public string Whatsapp { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }


    }
}