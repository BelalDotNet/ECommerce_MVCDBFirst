using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
    }
}