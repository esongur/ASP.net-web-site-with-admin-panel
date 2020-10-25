using Ertunc_Tibbi_Cihaz_Web_Site.Models.DataContext;
using Ertunc_Tibbi_Cihaz_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ertunc_Tibbi_Cihaz_Web_Site.Controllers
{
    public class BlogKategoriController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: BlogKategori
        public ActionResult Index()
        {
            return View(db.BlogKategori.ToList());
        }
        
        // GET: BlogKategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogKategori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogKategoriId,BlogKategoriAd,BlogAciklama")] BlogKategori blogkategori)
        {
            if (ModelState.IsValid)
            {
                db.BlogKategori.Add(blogkategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogkategori);
        }

        // GET: BlogKategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogKategori kategori = db.BlogKategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: BlogKategori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogKategoriId,BlogKategoriAd,BlogAciklama")] BlogKategori blogkategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogkategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogkategori);
        }

        // GET: BlogKategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogKategori kategori = db.BlogKategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: BlogKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            BlogKategori kategori = db.BlogKategori.Find(id);
            db.BlogKategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
