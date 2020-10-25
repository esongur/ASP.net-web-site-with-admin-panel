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
    public class BiomedikalController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Biomedikal
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Biomedikal.Include("BiomedikalKategori").ToList().OrderByDescending(x=>x.BiomedikalId));
        }



        // GET: Biomedikal/Create
        public ActionResult Create()
        {
            ViewBag.BiomedikalKategoriId = new SelectList(db.BiomedikalKategori, "BiomedikalKategoriId", "BiomedikalKategoriAd"); // baglantılı oldugumuz tablodaki ihtiyacımız olan verileri taşıma işlemi gerçekleştiriyoruz.
            return View();
        }

        // POST: Biomedikal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Biomedikal biomedikal,HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım
                string biomedikalimgname = ResimURL.FileName + imginfo.Extension; // logonun adını alma
               
                img.Resize(600, 400); // logonun boyutu
                img.Save("~/Uploads/Biomedikal/" + biomedikalimgname); // logonun kaydedileceği klasörün adresi

                biomedikal.ResimURL = "/Uploads/Biomedikal/" + biomedikalimgname; // logo urlin yeri
            }
            db.Biomedikal.Add(biomedikal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Biomedikal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Biomedikal.Where(x => x.BiomedikalId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.BiomedikalKategoriId = new SelectList(db.BiomedikalKategori, "BiomedikalKategoriId", "BiomedikalKategoriAd"); // baglantılı oldugumuz tablodaki ihtiyacımız olan verileri taşıma işlemi gerçekleştiriyoruz.
            return View(b);
        }

        // POST: Biomedikal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Biomedikal biomedikal,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Biomedikal.Where(x => x.BiomedikalId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string biomedikalimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                    img.Resize(600, 400); // logonun boyutu
                    img.Save("~/Uploads/Biomedikal/" + biomedikalimgname); // logonun kaydedileceği klasörün adresi

                    b.ResimURL = "/Uploads/Biomedikal/" + biomedikalimgname; // logo urlin yeri
                }
                b.Baslik = biomedikal.Baslik;
                b.BiomedikalKategoriId = biomedikal.BiomedikalKategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(biomedikal);
        }

        // GET: Biomedikal/Delete/5
        public ActionResult Delete(int id)
        {
            var b = db.Biomedikal.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
            }
            db.Biomedikal.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Biomedikal/Delete/5
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
