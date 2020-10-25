using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Ertunc_Tibbi_Cihaz_Web_Site.Models.Model;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Models.DataContext
{
    public class ErtuncTibbiCihazDBContext:DbContext
    {
        public ErtuncTibbiCihazDBContext():base("ErtuncTibbiCihazDB")
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogKategori> BlogKategori { get; set; }
        public DbSet<Servis> Servis { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<UrunKategori> UrunKategori { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<Markalar> Markalar { get; set; }
        public DbSet <Slider> Sliders { get; set; }
        public DbSet <Biomedikal> Biomedikal { get; set; }
        public DbSet<BiomedikalKategori> BiomedikalKategori { get; set; }
        public DbSet<Sertifikalar> Sertifikalar { get; set; }
        public DbSet<Yetki> Yetki { get; set; }

    }
}