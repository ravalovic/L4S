using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Controllers
{
    public class LogsOfServicesController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: LogsOfServices
        public ActionResult DetailData(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            DateTime take = DateTime.Now.AddDays(-60);
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<CATLogsOfService> cAtDetailList = db.CATLogsOfService.OrderByDescending(d => d.DateOfRequest).Take(999).ToList();
            return View(cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
            
        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            DateTime fromDate;
            DateTime toDate;
            DateTime.TryParse(insertDateFrom, out fromDate);
            if (!DateTime.TryParse(insertDateTo, out toDate))
            {
                toDate = DateTime.Now;
            }
            List<CATLogsOfService> cAtDetailList = db.CATLogsOfService.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).Take(999).ToList();
            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.ToList();
            }
            return View("DetailData", cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
