using Ertunc_Tibbi_Cihaz_Web_Site.Models.DataContext;
using PagedList;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Controllers
{

    public class HomeController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Home
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index(int sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Sertifikalar = db.Sertifikalar.ToList();
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Urunlerimiz = db.Urunler.ToList().ToPagedList(sayfa,3);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);
            return View();
        }

        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Servis.ToList().OrderByDescending(x => x.ServisId));
        }
        [Route("Kurumsal")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hakkimizda = db.Hakkimizda.SingleOrDefault();

            return View(db.Hakkimizda.ToList());

        }

        [Route("Sertifika")]
        public ActionResult Sertifikalar()
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Sertifikalar.ToList());
        }

        [Route("Iletişim")]
        public ActionResult Iletisim()
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "ertunctibbicihazlar@gmail.com";
                WebMail.Password = "06684251As";
                WebMail.SmtpPort = 587;
                WebMail.Send("info@ertunctibbicihaz.com", konu, "Gönderen:" + email + "<br> <br>" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";

                Response.Redirect("/iletişim");
            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }
        [Route("İnsanKaynakları")]
        public ActionResult İnsanKaynaklari ()
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }
        [Route("Ürünler")]
        public ActionResult Urunler(int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            return View(db.Urunler.Include("UrunKategori").OrderByDescending(x => x.UrunlerId).ToPagedList(sayfa, 15));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Urunler(string adsoyad = null, string tel=null, string adres = null, string email = null, string urun = null,string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "ertunctibbicihazlar@gmail.com";
                WebMail.Password = "06684251As";
                WebMail.SmtpPort = 587;
                WebMail.Send("info@ertunctibbicihaz.com", "Ürün Talebi:"+"-"+urun, "<strong>Gönderen Kurum:</strong>"+email+"---" +adsoyad+ "<br>" + "<strong>Adres:</strong>" + adres+ "<br>" + "<strong>Telefon:</strong>" + tel + "<br>" + "<strong>Ürün:</strong>" + urun + "<br>" + "<strong>Mesaj:</strong>" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/Ürünler");

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }

        [Route("ÜrünlerKategori/{baslik}-{id:int}")]
        public ActionResult KategoriUrunler(int id, int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            var b = db.Urunler.Include("UrunKategori").OrderByDescending(x => x.UrunlerId).Where(x => x.UrunKategori.UrunKategoriId == id).ToPagedList(sayfa, 15);

            return View(b);
        }

        [Route("Ürünler/{baslik}-{id:int}")]
        public ActionResult UrunlerDetay(int id)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            var b = db.Urunler.Include("UrunKategori").Where(x => x.UrunlerId == id).SingleOrDefault();

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(b);

        }

        [Route("BiomedikalHizmetler")]
        public ActionResult Biomedikal(int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            return View(db.Biomedikal.Include("BiomedikalKategori").OrderByDescending(x => x.BiomedikalId).ToPagedList(sayfa, 15));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Biomedikal(string adsoyad = null, string tel = null, string adres = null, string email = null, string urun = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "ertunctibbicihazlar@gmail.com";
                WebMail.Password = "06684251As";
                WebMail.SmtpPort = 587;
                WebMail.Send("info@ertunctibbicihaz.com", "Teknik Servis Talebi:" + "-" + urun, "<strong>Gönderen Kurum:</strong>" + email + "---" + adsoyad + "<br>" + "<strong>Adres:</strong>" + adres + "<br>" + "<strong>Telefon:</strong>" + tel + "<br>" + "<strong>Ürün:</strong>" + urun + "<br>" + "<strong>Mesaj:</strong>" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/BiomedikalHizmetler");

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }


        [Route("BiomedikalHizmetlerKategori/{baslik}-{id:int}")]
        public ActionResult KategoriBiomedikal(int id, int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            var b = db.Biomedikal.Include("BiomedikalKategori").OrderByDescending(x => x.BiomedikalId).Where(x => x.BiomedikalKategori.BiomedikalKategoriId == id).ToPagedList(sayfa, 15);

            return View(b);
        }
        [Route("BlogPost")]
        public ActionResult Blog(int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Blog.Include("BlogKategori").OrderByDescending(x => x.BlogId).ToPagedList(sayfa, 5));
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            var b = db.Blog.Include("BlogKategori").Where(x => x.BlogId == id).SingleOrDefault();

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(b);

        }

        [Route("BlogPostKategori/{baslik}-{id:int}")]
        public ActionResult KategoriBlog(int id, int sayfa = 1)
        {
            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);
            ViewBag.Biomedikal = db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd);

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            var b = db.Blog.Include("BlogKategori").OrderByDescending(x => x.BlogId).Where(x => x.BlogKategori.BlogKategoriId == id).ToPagedList(sayfa, 5);

            return View(b);
        }

        public ActionResult SliderPartial()
        {
            return View(db.Sliders.ToList().OrderByDescending(x => x.SliderId));
        }


        public ActionResult UrunKategoriPartial()
        {
            return PartialView(db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd));
        }


        public ActionResult UrunKayitPartial(int sayfa=1)
        {
            return PartialView(db.Urunler.ToList().OrderByDescending(x => x.UrunlerId).ToPagedList(sayfa,3));
        }


        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.BlogKategori.Include("Blogs").ToList().OrderBy(x => x.BlogKategoriAd));
        }


        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogId));
        }

        public ActionResult BiomedikalKategoriPartial()
        {
            return PartialView(db.BiomedikalKategori.Include("Biomedikals").ToList().OrderBy(x => x.BiomedikalKategoriAd));

        }


        public ActionResult BiomedikalKayitPartial(int sayfa = 1)
        {
            return PartialView(db.Biomedikal.ToList().OrderByDescending(x => x.BiomedikalId).ToPagedList(sayfa, 3));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            ViewBag.Servis = db.Servis.ToList().OrderByDescending(x => x.ServisId);

            ViewBag.Urunler = db.UrunKategori.Include("Urunlers").ToList().OrderBy(x => x.UrunKategoriAd);

                //db.Urunler.ToList().OrderByDescending(x => x.UrunlerId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

            return PartialView();
        }
    }
}