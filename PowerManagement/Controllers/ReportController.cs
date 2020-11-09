using PowerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerManagement.Controllers
{
    public class ReportController : Controller
    {
        DateTime? fromDate = DateTime.Now.Date;
        DateTime? toDate = DateTime.Now.Date.AddDays(1).AddTicks(-1);
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chart()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    if (toDate < fromDate) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
                    ViewBag.fromDate = fromDate;
                    ViewBag.toDate = toDate;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        [HttpGet]
        public JsonResult GetReportChart()
        {
            using (DBModel db = new DBModel())
            {
                var data1 = db.report.Where(p => p.tencb.Equals("Tu 1")).ToList();
                var data2 = db.report.Where(i => i.tencb.Equals("Tu 2")).ToList();
                var data3 = db.report.Where(r => r.tencb.Equals("Tu 3")).ToList();
                var data = new List<object>();
                data.Add(data1);
                data.Add(data2);
                data.Add(data3);
                return Json(data2, JsonRequestBehavior.AllowGet);
            }
        }

    }
}