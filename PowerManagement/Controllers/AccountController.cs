using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PowerManagement.Models;

namespace PowerManagement.Controllers
{
    public class AccountController : Controller
    {
        DBModel db = new DBModel();
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccLogin objUser)
        {
            if (ModelState.IsValid)
            {
                using (DBModel db = new DBModel())
                {
                    var obj = db.taikhoan.Where(a => a.user.Equals(objUser.user) && a.pass.Equals(objUser.pass)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.user;
                        Session["user_name"] = obj.user.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Email hoặc mật khẩu không chính xác.";
                        return View();
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}