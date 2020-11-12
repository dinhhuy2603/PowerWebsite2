using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;
using PowerManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerManagement.Controllers
{
    public class AlarmController : Controller
    {
        // GET: Alarm
        public ActionResult Index(DateTime? fromDate, DateTime? toDate, int? tencb)
        {
            if (Session["UserID"] != null)
            {
                using (DBModel db = new DBModel())
                {
                    var alarms = new List<Alarm>();
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
                        if(toDate < fromDate)
                        {
                            alarms = db.alarm.Where(x => x.thoigian <= fromDate && x.thoigian >= toDate).OrderBy(p => p.thoigian).ToList();
                        }
                        else
                        {
                            toDate = toDate.GetValueOrDefault(DateTime.Now.Date).Date.AddHours(23).AddMinutes(59);
                            alarms = db.alarm.Where(x => x.thoigian <= toDate && x.thoigian >= fromDate).OrderBy(p => p.thoigian).ToList();
                        }
                    }
                    else
                    {
                        if (!fromDate.HasValue)
                        {
                            fromDate = DateTime.Now.Date;
                            if (!toDate.HasValue)
                            {
                                alarms = db.alarm.OrderBy(p => p.thoigian).ToList();
                                toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
                            }
                            else
                            {
                                toDate = toDate.GetValueOrDefault(DateTime.Now.Date).Date.AddHours(23).AddMinutes(59);
                                alarms = db.alarm.Where(x => x.thoigian <= toDate).OrderBy(p => p.thoigian).ToList();
                            }
                        }
                        else
                        {
                            if (!toDate.HasValue) {
                                alarms = db.alarm.Where(x => x.thoigian >= fromDate).OrderBy(p => p.thoigian).ToList();
                                toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
                            }
                        }

                       
                        
                    }
                    var result = new List<Alarm>();
                    if(name != "")
                    {
                        alarms = alarms.Where(x => x.tencb.Equals(name)).ToList();
                    }
                    
                    Session["alarms"] = alarms;
                    ViewBag.Recoders = alarms;
                    ViewBag.fromDate = fromDate;
                    ViewBag.toDate = toDate;
                    ViewBag.listCB = listCB;
                    return View(alarms);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ExportToExcel()
        {
            var recoders = (List<Alarm>)Session ["alarms"];
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();

            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Alarm list");
            Sheet.Cells["A1"].Value = "Thời gian";
            Sheet.Cells["B1"].Value = "Tên CB";
            Sheet.Cells["C1"].Value = "Giá trị thấp";
            Sheet.Cells["D1"].Value = "Giá trị hiện tại";
            Sheet.Cells["E1"].Value = "Giá trị cao";
            Sheet.Cells["F1"].Value = "Cảnh báo";
            int row = 2;
            foreach (var item in recoders)
            {
                Sheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";

                Sheet.Cells[string.Format("A{0}", row)].Value = item.thoigian;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.tencb;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.giatrithap;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.giatrihientai;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.giatricao;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.canhbao;
                row++;
            }
            Sheet.Column(1).Width = 50;
            Sheet.Column(2).Width = 40;
            Sheet.Column(3).Width = 40;
            Sheet.Cells["A1:F1"].Style.Font.Size = 12;
            Sheet.Cells["A1:F1"].Style.Font.Bold = true;
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