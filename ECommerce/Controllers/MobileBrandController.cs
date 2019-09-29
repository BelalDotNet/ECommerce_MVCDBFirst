using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class MobileBrandController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        
        // GET: MobileBrand
        public ActionResult Index()
        {
            return View(db.MobileBrands.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken ]
        public async Task<ActionResult> Create(MobileBrand mobileBrand)
        {
            if(ModelState.IsValid)
            {
                db.MobileBrands.Add(mobileBrand);
               await db.SaveChangesAsync();
                return RedirectToAction("Index");
                    
            }
            return View(mobileBrand);
        }





        public ActionResult Delete(int? id)
        {
            MobileBrand mobileBrand = db.MobileBrands.Find(id);
            if (mobileBrand == null)
            {
                return HttpNotFound();
            }
            return View(mobileBrand);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles ="Admin")]
        public ActionResult DeleteConfirm(int? id)
        {
            MobileBrand mobileBrand = db.MobileBrands.Find(id);
            if (ModelState.IsValid)
            {
                db.MobileBrands.Remove(mobileBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mobileBrand);
        }



        public ActionResult Details(int? id)
        {
            MobileBrand mobileBrand = db.MobileBrands.Find(id);
            if (mobileBrand == null)
            {
                return HttpNotFound();
            }
            return View(mobileBrand);
        }

    }
}