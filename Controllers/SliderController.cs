using Ertunc_Tibbi_Cihaz_Web_Site.Models.DataContext;
using Ertunc_Tibbi_Cihaz_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Controllers
{
    public class SliderController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }
        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SliderId,Baslik,Aciklama,ResimURL")] Slider slider, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {

                    WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                    img.Resize(1900, 520); // logonun boyutu
                    img.Save("~/Uploads/Slider/" + sliderimgname); // logonun kaydedileceği klasörün adresi

                    slider.ResimURL = "/Uploads/Slider/" + sliderimgname; // logo urlin yeri
                }
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SliderId,Baslik,Aciklama,ResimURL")] Slider slider, int id, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var s = db.Sliders.Where(x => x.SliderId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(s.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                    img.Resize(1024, 360); // logonun boyutu
                    img.Save("~/Uploads/Slider/" + sliderimgname); // logonun kaydedileceği klasörün adresi

                    s.ResimURL = "/Uploads/Slider/" + sliderimgname; // logo urlin yeri
                }
                s.Baslik = slider.Baslik;
                s.Aciklama = slider.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Slider slider = db.Sliders.Find(id);
            if (System.IO.File.Exists(Server.MapPath(slider.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(slider.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
            }
            db.Sliders.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
