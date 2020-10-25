using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        [DisplayName("Blog Adı")]
        [Required,StringLength(200,ErrorMessage ="200 Karakter Olmalıdır...")]
        public string Baslik { get; set; }
        [DisplayName("Blog Açıklama")]
        [Required,MaxLength]
        public string Aciklama { get; set; }
        [DisplayName("Blog Resim")]
        public string ResimURL { get; set; }
        public int BlogKategoriId { get; set; }
        public BlogKategori  BlogKategori { get; set; }
    }
}