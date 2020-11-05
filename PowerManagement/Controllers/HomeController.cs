using PowerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    var hienthi = db.hienthi.FirstOrDefault();
                    var trangthai = db.trangthai.FirstOrDefault();
                    ViewBag.hienthi = !string.IsNullOrEmpty(hienthi.cb1) ? hienthi : new Hienthiweb();
                    ViewBag.trangthai = !string.IsNullOrEmpty(trangthai.status_cb1) ? trangthai : new Trangthai();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }
    }
}