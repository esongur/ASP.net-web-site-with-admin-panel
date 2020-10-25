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
    public class IletisimController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Iletisim
        public ActionResult Index()
        {
            return View( db.Iletisim.ToList());
        }


        // GET: Iletisim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisim/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IletisimId,Adres,Telefon,Fax,Email,Whatsapp,Instagram,Facebook,Twitter")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisim);

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
