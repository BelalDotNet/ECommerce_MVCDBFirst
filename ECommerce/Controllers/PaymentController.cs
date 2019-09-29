using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class PaymentController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        // GET: Payment
        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }
    }
}