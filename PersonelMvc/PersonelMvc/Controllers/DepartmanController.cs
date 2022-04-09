using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMvc.Models.EntityFramework;

namespace PersonelMvc.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman

        PersonelMvcEntities db = new PersonelMvcEntities(); // bütün database bilgilerimi db ye aldım

        public ActionResult Index()
        {
            var model = db.Departman.ToList(); // hepsini getir
            return View(model); // model ı wiew e gönderiyoruz
        }

        [HttpGet] // veriyi aldığı kısım okuduğu kısım
        public ActionResult Yeni()
        {
            return View("DepartmanForm"); // view ile metodun adı illa aynı olmak zorunda değil
        }

        [HttpPost] // veriyi yazıyoruz  // (FormMethod.Post basıldığında bu alan çalışır)
        public ActionResult Kaydet(Departman departman)
        {
            if (departman.Id == 0)  // id 0 ise yeni kayıt
            {
                db.Departman.Add(departman);
            }
            else  // idsi olan kayıdı güncelle (değilse var olan kayıdı güncelle)
            {
                var guncellenecekdepartman = db.Departman.Find(departman.Id);

                if (guncellenecekdepartman == null)
                {
                    return HttpNotFound();
                }

                guncellenecekdepartman.Ad = departman.Ad;
            }

            db.SaveChanges();

            return RedirectToAction("Index","Departman"); // işlem olunca departman controller da ki Index e gönder (form_load gibi)
        }



        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);

            if (model == null)
            {
                return HttpNotFound();
                
            }

            

            return View("DepartmanForm", model);
        }



        public ActionResult Sil(int id)
        {
            var silinicekdepartman = db.Departman.Find(id);

            if (silinicekdepartman == null)
            {
                return HttpNotFound();
            }

            db.Departman.Remove(silinicekdepartman);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}