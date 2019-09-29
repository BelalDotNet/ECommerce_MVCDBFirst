using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Microsoft.AspNet.Identity; // Add for Roles Configure
using Microsoft.AspNet.Identity.EntityFramework; // Add for roles Configure
using ECommerce.Models; //Add for Roles Configure
using System.Data.Entity; // for entity



namespace ECommerce.Controllers
{
    public class RoleController : Controller
    {



        // GET: Roles
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //Populate DropDownList 
            var context = new ApplicationDbContext();

            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;

            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            ViewBag.Message = "";
            return View();
        }




        //Get Create 
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }



        // Get Post
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.Message = "Role Created Successfully!!!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // Delete 
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string RoleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }




        // Get Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);

            return View(thisRole);
        }


        // Post Edit 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }



        // Add Roles To The Users
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var context = new ApplicationDbContext();

            if (context == null)
            {
                throw new ArgumentNullException("context", "Context must not be null.");
            }

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id, RoleName);

            ViewBag.Message = "Role created successfully !";

            // Repopulate Dropdown Lists
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            return View("Index");
        }



        // Get Roles List For a user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var context = new ApplicationDbContext();
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);

                // Repopulate Dropdown Lists
                var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = rolelist;
                var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
                new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = userlist;

                ViewBag.Message = "Roles retrieved successfully !";
            }

            return View("Index");
        }


        // Delete Roles From The Users
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            var context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.IsInRole(user.Id, RoleName))
            {
                userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.Message = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.Message = "This user doesn't belong to selected role.";
            }

            // Repopulate Dropdown Lists
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            return View("Index");
        }

    }
}