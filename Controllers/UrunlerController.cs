using Ertunc_Tibbi_Cihaz_Web_Site.Models.DataContext;
using Ertunc_Tibbi_Cihaz_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Controllers
{
    public class UrunlerController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Urunler
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Urunler.Include("UrunKategori").ToList().OrderByDescending(x => x.UrunlerId));
        }


        // GET: Urunler/Create
        public ActionResult Create()
        {
            ViewBag.UrunKategoriId = new SelectList(db.UrunKategori, "UrunKategoriId", "UrunKategoriAd"); // baglantılı oldugumuz tablodaki ihtiyacımız olan verileri taşıma işlemi gerçekleştiriyoruz.
            return View();
        }

        // POST: Urunler/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urunler, HttpPostedFileBase ResimURL1, HttpPostedFileBase ResimURL2, HttpPostedFileBase ResimURL3)
        {
            if (ResimURL1 != null)
            {

                WebImage img1 = new WebImage(ResimURL1.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo1 = new FileInfo(ResimURL1.FileName); // logonun bilgilerini aldıgımız kısım

                string urunlerimgname1= Guid.NewGuid().ToString() + imginfo1.Extension; // logonun adını alma
                img1.Resize(300, 300,true,false); // logonun boyutu
                img1.Save("~/Uploads/Urunler/" + urunlerimgname1); // logonun kaydedileceği klasörün adresi

                urunler.UrunlerResimURL1 = "/Uploads/Urunler/" + urunlerimgname1; // logo urlin yeri
            }
            if (ResimURL2 != null)
            {

                WebImage img2 = new WebImage(ResimURL2.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo2 = new FileInfo(ResimURL2.FileName); // logonun bilgilerini aldıgımız kısım

                string urunlerimgname2 = Guid.NewGuid().ToString() + imginfo2.Extension; // logonun adını alma
                img2.Resize(600, 400); // logonun boyutu
                img2.Save("~/Uploads/Urunler/" + urunlerimgname2); // logonun kaydedileceği klasörün adresi

                urunler.UrunlerResimURL2 = "/Uploads/Urunler/" + urunlerimgname2; // logo urlin yeri
            }
            if (ResimURL3 != null)
            {

                WebImage img3 = new WebImage(ResimURL3.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo3 = new FileInfo(ResimURL3.FileName); // logonun bilgilerini aldıgımız kısım

                string urunlerimgname3 = Guid.NewGuid().ToString() + imginfo3.Extension; // logonun adını alma
                img3.Resize(600, 400); // logonun boyutu
                img3.Save("~/Uploads/Urunler/" + urunlerimgname3); // logonun kaydedileceği klasörün adresi

                urunler.UrunlerResimURL3 = "/Uploads/Urunler/" + urunlerimgname3; // logo urlin yeri
            }
            db.Urunler.Add(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Urunler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var u = db.Urunler.Where(x => x.UrunlerId == id).SingleOrDefault();
            if (u == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunKategoriId = new SelectList(db.UrunKategori, "UrunKategoriId", "UrunKategoriAd", u.UrunKategoriId);
            return View(u);
        }

        // POST: Urunler/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Urunler urunler, HttpPostedFileBase UrunlerResimURL1, HttpPostedFileBase UrunlerResimURL2, HttpPostedFileBase UrunlerResimURL3)
        {
            if (ModelState.IsValid)
            {
                var u = db.Urunler.Where(x => x.UrunlerId == id).SingleOrDefault();
                if (UrunlerResimURL1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL1))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL1)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img1 = new WebImage(UrunlerResimURL1.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo1= new FileInfo(UrunlerResimURL1.FileName); // logonun bilgilerini aldıgımız kısım

                    string blogimgname1 = Guid.NewGuid().ToString() + imginfo1.Extension; // logonun adını alma
                    img1.Resize(300, 300, true, false); // logonun boyutu
                    img1.Save("~/Uploads/Urunler/" + blogimgname1); // logonun kaydedileceği klasörün adresi

                    u.UrunlerResimURL1 = "/Uploads/Urunler/" + blogimgname1; // logo urlin yeri
                }

                if (UrunlerResimURL2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL2))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL2)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img2 = new WebImage(UrunlerResimURL2.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo2 = new FileInfo(UrunlerResimURL2.FileName); // logonun bilgilerini aldıgımız kısım

                    string blogimgname2 = Guid.NewGuid().ToString() + imginfo2.Extension; // logonun adını alma
                    img2.Resize(600, 400); // logonun boyutu
                    img2.Save("~/Uploads/Urunler/" + blogimgname2); // logonun kaydedileceği klasörün adresi

                    u.UrunlerResimURL2 = "/Uploads/Urunler/" + blogimgname2; // logo urlin yeri
                }

                if (UrunlerResimURL3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL3))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL3)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img3 = new WebImage(UrunlerResimURL3.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo3 = new FileInfo(UrunlerResimURL3.FileName); // logonun bilgilerini aldıgımız kısım

                    string blogimgname3 = Guid.NewGuid().ToString() + imginfo3.Extension; // logonun adını alma
                    img3.Resize(600, 400); // logonun boyutu
                    img3.Save("~/Uploads/Urunler/" + blogimgname3); // logonun kaydedileceği klasörün adresi

                    u.UrunlerResimURL3 = "/Uploads/Urunler/" + blogimgname3; // logo urlin yeri
                }


                u.UrunlerBaslik = urunler.UrunlerBaslik;
                u.UrünlerAciklama = urunler.UrünlerAciklama;
                u.UrunKategoriId = urunler.UrunKategoriId;
                u.UrunlerFiyat = urunler.UrunlerFiyat;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urunler);
        }

        // GET: Urunler/Delete/5
        public ActionResult Delete(int id)
        {
            var u = db.Urunler.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL1))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL1)); // daha önceki kaydı veritabanından siliyoruz.
            }
            if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL2))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL2)); // daha önceki kaydı veritabanından siliyoruz.
            }
            if (System.IO.File.Exists(Server.MapPath(u.UrunlerResimURL3))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(u.UrunlerResimURL3)); // daha önceki kaydı veritabanından siliyoruz.
            }
            db.Urunler.Remove(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Urunler/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
