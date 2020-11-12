using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        public ActionResult Index(DateTime? fromDate, DateTime? toDate, int? tencb)
        {
            if (Session["UserID"] != null)
            {
                using (DBModel db = new DBModel())
                {
                    var reports = new List<Report>();
                    var listCB = new List<SelectListItem>
                    {
                        new SelectListItem{ Text="Tất cả", Value = "0", Selected = true },
                        new SelectListItem{ Text="Tu 1", Value = "1" },
                        new SelectListItem{ Text="Tu 2", Value = "2" },
                        new SelectListItem{ Text="Tu 3", Value = "3" },
                        new SelectListItem{ Text="Nguon Dien", Value = "4" },
                    };

                    string name = "";
                    if (!string.IsNullOrEmpty(tencb.ToString()) && tencb > 0)
                    {
                        name = listCB[(int)tencb].Text.ToString();
                    }
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        if (toDate < fromDate)
                        {
                            reports = db.report.Where(x => x.thoigian <= fromDate && x.thoigian >= toDate).OrderBy(p => p.thoigian).ToList();
                        }
                        else
                        {
                            toDate = toDate.GetValueOrDefault(DateTime.Now.Date).Date.AddHours(23).AddMinutes(59);
                            reports = db.report.Where(x => x.thoigian <= toDate && x.thoigian >= fromDate).OrderBy(p => p.thoigian).ToList();
                        }
                    }
                    else
                    {
                        if (!fromDate.HasValue)
                        {
                            fromDate = DateTime.Now.Date;
                            if (!toDate.HasValue)
                            {
                                reports = db.report.OrderBy(p => p.thoigian).ToList();
                                toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
                            }
                            else
                            {
                                toDate = toDate.GetValueOrDefault(DateTime.Now.Date).Date.AddHours(23).AddMinutes(59);
                                reports = db.report.Where(x => x.thoigian <= toDate).OrderBy(p => p.thoigian).ToList();
                            }
                        }
                        else
                        {
                            if (!toDate.HasValue)
                            {
                                reports = db.report.Where(x => x.thoigian >= fromDate).OrderBy(p => p.thoigian).ToList();
                                toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
                            }
                        }



                    }
                    var result = new List<Alarm>();
                    if (name != "")
                    {
                        reports = reports.Where(x => x.tencb.Equals(name)).ToList();
                    }

                    Session["reports"] = reports;
                    ViewBag.Recoders = reports;
                    ViewBag.fromDate = fromDate;
                    ViewBag.toDate = toDate;
                    ViewBag.listCB = listCB;
                    return View(reports);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
                var data1 = db.report.Where(p => p.tencb.Equals("Tu 1")).OrderBy(p => p.thoigian).ToList();
                var data2 = db.report.Where(i => i.tencb.Equals("Tu 2")).OrderBy(i => i.thoigian).ToList();
                var data3 = db.report.Where(r => r.tencb.Equals("Tu 3")).OrderBy(r => r.thoigian).ToList();
                var data = new List<object>();
                data.Add(data1);
                data.Add(data2);
                data.Add(data3);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportToExcel()
        {
            var recoders = (List<Report>)Session["reports"];
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();

            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report list");
            Sheet.Cells["A1"].Value = "Thời gian";
            Sheet.Cells["B1"].Value = "Tên CB";
            Sheet.Cells["C1"].Value = "Giá trị";
            int row = 2;
            foreach (var item in recoders)
            {
                Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";

                Sheet.Cells[string.Format("A{0}", row)].Value = item.thoigian;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.tencb;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.giatri;
                row++;
            }
            Sheet.Column(1).Width = 50;
            Sheet.Column(2).Width = 40;
            Sheet.Column(3).Width = 40;
            Sheet.Cells["A1:C1"].Style.Font.Size = 12;
            Sheet.Cells["A1:C1"].Style.Font.Bold = true;
            Sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=\"ReportWater.xlsx\"");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
            return RedirectToAction("Index", "Alarm");
        }

    }
}