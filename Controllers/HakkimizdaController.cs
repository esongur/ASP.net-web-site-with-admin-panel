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
    public class HakkimizdaController : Controller
    {
        ErtuncTibbiCihazDBContext db = new ErtuncTibbiCihazDBContext();

        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }


        // GET: Hakkimizda/Edit/5
        public ActionResult Edit(int id)
        {
            var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
            return View(hakkimizda);
        }

        // POST: Hakkimizda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda hakkimizda, HttpPostedFileBase ResimUrl1, HttpPostedFileBase ResimUrl2, HttpPostedFileBase ResimUrl3)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                if (ResimUrl1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl1)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl1));
                    
                    }
                    WebImage img1 = new WebImage(ResimUrl1.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo1 = new FileInfo(ResimUrl1.FileName); // logonun bilgilerini aldıgımız kısım

                    string logoname = ResimUrl1.FileName + imginfo1.Extension; // logonun adını alma
                    img1.Resize(900, 632); // logonun boyutu
                    img1.Save("~/Uploads/Hakkimizda/" + logoname); // logonun kaydedileceği klasörün adresi

                    h.ResimUrl1 = "/Uploads/Hakkimizda/" + logoname; // logo urlin yeri

                }


                if (ResimUrl2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl2)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl2));

                    }
                    WebImage img2 = new WebImage(ResimUrl2.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo2 = new FileInfo(ResimUrl2.FileName); // logonun bilgilerini aldıgımız kısım

                    string logoname = ResimUrl1.FileName + imginfo2.Extension; // logonun adını alma
                    img2.Resize(900, 632); // logonun boyutu
                    img2.Save("~/Uploads/Hakkimizda/" + logoname); // logonun kaydedileceği klasörün adresi

                    h.ResimUrl2 = "/Uploads/Hakkimizda/" + logoname; // logo urlin yeri

                }



                if (ResimUrl3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl3)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl3));

                    }
                    WebImage img3 = new WebImage(ResimUrl3.InputStream); //logo nesnesi oluşturma 
                    FileInfo imginfo2 = new FileInfo(ResimUrl3.FileName); // logonun bilgilerini aldıgımız kısım

                    string logoname = ResimUrl1.FileName + imginfo2.Extension; // logonun adını alma
                    img3.Resize(900,632); // logonun boyutu
                    img3.Save("~/Uploads/Hakkimizda/" + logoname); // logonun kaydedileceği klasörün adresi

                    h.ResimUrl3 = "/Uploads/Hakkimizda/" + logoname; // logo urlin yeri

                }

                h.Aciklama = hakkimizda.Aciklama;

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            
            return View(hakkimizda);
        }
    }
}

