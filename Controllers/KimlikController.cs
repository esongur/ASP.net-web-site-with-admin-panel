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
    public class KimlikController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
           
            return View(db.Kimlik.ToList());
        }


        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik, HttpPostedFileBase  LogoURL)
        {

            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
                if (LogoURL!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoURL));
                    }
                    WebImage img = new WebImage(LogoURL.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo = new FileInfo(LogoURL.FileName); // logonun bilgilerini aldıgımız kısım

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension; // logonun adını alma
                    img.Resize(300, 150); // logonun boyutu
                    img.Save("~/Uploads/Kimlik/" + logoname); // logonun kaydedileceği klasörün adresi

                    k.LogoURL = "/Uploads/Kimlik/" + logoname; // logo urlin yeri

                }
                k.Title = kimlik.Title;
                k.Keywords = kimlik.Keywords;
                k.Description = kimlik.Description;
                k.Unvan = kimlik.Unvan;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kimlik);
        }




    }
}
