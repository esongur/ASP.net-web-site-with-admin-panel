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
    public class BlogController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Blog
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("BlogKategori").ToList().OrderByDescending(x => x.BlogId));
        }



        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.BlogKategoriId = new SelectList(db.BlogKategori, "BlogKategoriId", "BlogKategoriAd"); // baglantılı oldugumuz tablodaki ihtiyacımız olan verileri taşıma işlemi gerçekleştiriyoruz.
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                img.Resize(600, 400); // logonun boyutu
                img.Save("~/Uploads/Blog/" + blogimgname); // logonun kaydedileceği klasörün adresi

                blog.ResimURL = "/Uploads/Blog/" + blogimgname; // logo urlin yeri
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogKategoriId = new SelectList(db.BlogKategori, "BlogKategoriId", "BlogKategoriAd"); // baglantılı oldugumuz tablodaki ihtiyacımız olan verileri taşıma işlemi gerçekleştiriyoruz.
            return View(b);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Blog blog, HttpPostedFile ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
                    }
                    WebImage img = new WebImage(ResimURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo = new FileInfo(ResimURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                    img.Resize(600, 400); // logonun boyutu
                    img.Save("~/Uploads/Blog/" + blogimgname); // logonun kaydedileceği klasörün adresi

                    b.ResimURL = "/Uploads/Blog/" + blogimgname; // logo urlin yeri
                }
                b.Baslik = blog.Baslik;
                b.Aciklama = blog.Aciklama;
                b.BlogKategoriId = blog.BlogKategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);

        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL))) //Veri tabanımızda daha once logoURL olup olmadıgını kontrol ediyoruz
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL)); // daha önceki kaydı veritabanından siliyoruz.
            }
            db.Blog.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Blog/Delete/5
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
