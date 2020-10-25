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
    public class UrunKategoriController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: UrunKategori
        public ActionResult Index()
        {
            return View(db.UrunKategori.ToList());
        }

        // GET: UrunKategori/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UrunKategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrunKategori/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunKategoriId,UrunKategoriAd,UrunAciklama")] UrunKategori urunkategori)
        {
            if (ModelState.IsValid)
            {
                db.UrunKategori.Add(urunkategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(urunkategori);
        }

        // GET: UrunKategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunKategori urunkategori = db.UrunKategori.Find(id);
            if (urunkategori == null)
            {
                return HttpNotFound();
            }
            return View(urunkategori);
        }

        // POST: UrunKategori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "UrunKategoriId,UrunKategoriAd,UrunAciklama")] UrunKategori urunkategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunkategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urunkategori);
        }

        // GET: UrunKategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunKategori urunkategori = db.UrunKategori.Find(id);
            if (urunkategori == null)
            {
                return HttpNotFound();
            }
            return View(urunkategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UrunKategori urunkategori= db.UrunKategori.Find(id);
            db.UrunKategori.Remove(urunkategori);
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
