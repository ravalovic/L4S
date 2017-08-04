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
            if (fromDate == toDate) toDate = toDate.AddDays(1);
            List<CATLogsOfService> cAtDetailList = db.CATLogsOfService.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).Take(999).ToList();
            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.ToList();
            }
            return View("DetailData", cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }

        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText)
        {
            bool datCondition = false;
            bool textCondition = false;
            int searchID;
            List<CATLogsOfService> cAtDetailList = new List<CATLogsOfService>();
            int.TryParse(searchText, out searchID);
            if (insertDateFrom != null) datCondition = true;
            if (searchText != null) textCondition = true;

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
            if (fromDate == toDate) toDate = toDate.AddDays(1);

            if (datCondition && !textCondition)
            {
                cAtDetailList = db.CATLogsOfService.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && !datCondition)
            {
                cAtDetailList = db.CATLogsOfService.Where(p => p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.CustomerID == searchID).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && datCondition)
            {
                cAtDetailList = db.CATLogsOfService.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText) || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).ToList();
            }

            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.OrderByDescending(d => d.DateOfRequest).ToList();
            }
            return View("DetailData", cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }

        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
            int pageNumber = (page ?? 1);

            var startDate = new DateTime(reqDate.Year, reqDate.Month, 1);
            var endDate = startDate.AddDays(1).AddTicks(-1);


            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }

            List<CATLogsOfService> cAtDetailList = db.CATLogsOfService.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == custId && p.ServiceID == servId).OrderBy(d => d.DateOfRequest).ToList();
            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.Take(999).ToList();
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
