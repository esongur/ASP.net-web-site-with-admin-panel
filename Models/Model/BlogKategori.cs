using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.Model
{
    [Table("BlogKategori")]
    public class BlogKategori
    {
            [Key]
            public int BlogKategoriId { get; set; }
            [Required,StringLength(200,ErrorMessage ="200 Karakter Olmalı...")]
            [DisplayName("Blog Kategori Adı")]
            public string BlogKategoriAd { get; set; }
            [Required,MaxLength]
            [DisplayName("Blog Kategori Açıklama")]
            public string BlogAciklama { get; set; }
            public ICollection<Blog> Blogs { get; set; }


    }
}