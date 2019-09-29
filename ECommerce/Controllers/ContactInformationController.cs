using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ECommerce.Models;
using System.Threading.Tasks;

using ECommerce.ContactInformationVM;

using AutoMapper; 

namespace ECommerce.Controllers
{
    public class ContactInformationController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        // GET: ContactInformation
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.ContactInformations.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        
        public  ActionResult Create(ContactVM contactVM)
        {
           
                if (ModelState.IsValid)
                {
                var conta = Mapper.Map<ContactInformation>(contactVM);
                    db.ContactInformations.Add(conta);
                    db.SaveChanges();
                    
                   
                }
            

            return RedirectToAction("Create");

        }


        public ActionResult Delete(int? id)
        {
            ContactInformation contactInformation = db.ContactInformations.Find(id);
            if(contactInformation==null)
            {
                return HttpNotFound();
            }
            return View(contactInformation);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            ContactInformation contactInformation = db.ContactInformations.Find(id);
            if (ModelState.IsValid)
            {
                db.ContactInformations.Remove(contactInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactInformation);
        }



        public ActionResult Details(int? id)
        {
            ContactInformation contactInformation = db.ContactInformations.Find(id);
            if (contactInformation == null)
            {
                return HttpNotFound();
            }
            return View(contactInformation);
        }


    }
}