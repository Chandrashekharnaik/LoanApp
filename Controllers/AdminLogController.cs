using LoanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoanApp.Controllers
{
    public class AdminLogController : Controller
    {
        // GET: AdminLog
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin obj)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(obj.AdminName, obj.Rememberme);
                return RedirectToAction("Index", "Admin");
            }
            
            return View(obj);
        }
    }
}