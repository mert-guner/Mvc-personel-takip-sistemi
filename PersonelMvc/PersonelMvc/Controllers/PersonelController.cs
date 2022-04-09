using PersonelMvc.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMvc.ViewModels;


namespace PersonelMvc.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        PersonelMvcEntities db = new PersonelMvcEntities();

        
  
        public ActionResult Index()
        {
            var model = db.Personel.Include(x => x.Departman).ToList();
            
            return View(model);
        }

        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList()
            };

            return View("PersonelForm", model);
            
        }

        public ActionResult Kaydet(Personel personel)
        {
            if (personel.Id == 0)
            {
                db.Personel.Add(personel);
            }
            else
            {
                // bu satırda id bilgisini almadan o an ki kayıdın güncellenmesini sağlıyoruz.
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }

            // kayıt işlemi yapılıyor
            db.SaveChanges();

            // index sayfasına yönlendiriyoruz
            return RedirectToAction("Index");
        }


        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)

            };

            return View("PersonelForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekpersonel = db.Personel.Find(id);

            if (silinecekpersonel == null)
            {
                return HttpNotFound();
            }

            db.Personel.Remove(silinecekpersonel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}