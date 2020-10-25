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
    public class MarkalarController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Markalar
        public ActionResult Index()
        {
            return View(db.Markalar.ToList());
        }

        // GET: Markalar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markalar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Markalar markalar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {

                if (ResimURL != null)
                {

                    WebImage img1 = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo1 = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string urunlerimgname1 = Guid.NewGuid().ToString() + imginfo1.Extension; // logonun adını alma
                    img1.Resize(600, 400); // logonun boyutu
                    img1.Save("~/Uploads/Markalar/" + urunlerimgname1); // logonun kaydedileceği klasörün adresi

                    markalar.ResimURL = "/Uploads/Markalar/" + urunlerimgname1; // logo urlin yeri
                }

            }
            db.Markalar.Add(markalar);
            db.SaveChanges();
            return RedirectToAction("Index");

        } 
           
        

        // GET: Markalar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // POST: Markalar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Markalar markalar, HttpPostedFile ResimURL)
        {
            var m = db.Markalar.Where(x => x.MarkalarId == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(m.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(m.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img1 = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo1 = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string urunlerimgname1 = Guid.NewGuid().ToString() + imginfo1.Extension; // logonun adını alma
                    img1.Resize(600, 400); // logonun boyutu
                    img1.Save("~/Uploads/Markalar/" + urunlerimgname1); // logonun kaydedileceği klasörün adresi

                    markalar.ResimURL = "/Uploads/Markalar/" + urunlerimgname1; // logo urlin yeri
                }
                m.Ad = markalar.Ad;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markalar);
        }

        // GET: Markalar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Markalar/Delete/5
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
