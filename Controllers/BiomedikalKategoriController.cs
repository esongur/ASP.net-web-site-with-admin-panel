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
    public class BiomedikalKategoriController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();

        // GET: BiomedikalKategori
        public ActionResult Index()
        {
            return View(db.BiomedikalKategori.ToList());
        }


        // GET: BiomedikalKategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BiomedikalKategori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="BiomedikalKategoriId,BiomedikalKategoriAd")] BiomedikalKategori biomedikalkategori)
        {
            if (ModelState.IsValid)
            {
                db.BiomedikalKategori.Add(biomedikalkategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(biomedikalkategori);
        }

        // GET: BiomedikalKategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiomedikalKategori kategori = db.BiomedikalKategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: BiomedikalKategori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BiomedikalKategoriId,BiomedikalKategoriAd")] BiomedikalKategori BiomedikalKategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(BiomedikalKategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(BiomedikalKategori);
        }

        // GET: BiomedikalKategori/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BiomedikalKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            BiomedikalKategori kategori = db.BiomedikalKategori.Find(id);
            db.BiomedikalKategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
