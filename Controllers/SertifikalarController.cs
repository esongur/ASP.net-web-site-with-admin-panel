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
    public class SertifikalarController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();

        // GET: Sertifikalar
        public ActionResult Index()
        {
            return View(db.Sertifikalar.ToList());
        }


        // GET: Sertifikalar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sertifikalar/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sertifikalar sertifikalar, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                img.Resize(600, 400); // logonun boyutu
                img.Save("~/Uploads/Sertifika/" + blogimgname); // logonun kaydedileceği klasörün adresi

                sertifikalar.ResimURL = "/Uploads/Sertifika/" + blogimgname; // logo urlin yeri
            }
            db.Sertifikalar.Add(sertifikalar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Sertifikalar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Sertifikalar.Where(x => x.SertifikaId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        // POST: Sertifikalar/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sertifikalar sertifikalar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var u = db.Sertifikalar.Where(x => x.SertifikaId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(u.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(u.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img1 = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo1 = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string blogimgname1 = Guid.NewGuid().ToString() + imginfo1.Extension; // logonun adını alma
                    img1.Resize(600, 400); // logonun boyutu
                    img1.Save("~/Uploads/Sertifika/" + blogimgname1); // logonun kaydedileceği klasörün adresi

                    u.ResimURL = "/Uploads/Sertifika/" + blogimgname1; // logo urlin yeri
                }

                u.Ad = sertifikalar.Ad;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sertifikalar);

        }
        // GET: Sertifikalar/Delete/5
        public ActionResult Delete(int id)
        {
            var b = db.Sertifikalar.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
            }
            db.Sertifikalar.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Sertifikalar/Delete/5
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
