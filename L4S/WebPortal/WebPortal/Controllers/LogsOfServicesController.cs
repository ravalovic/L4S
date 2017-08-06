using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
        public ActionResult DetailData(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime currentDate)
        {
            int pageNumber = (page ?? 1);
            if (currentCustId !=0 && currentServId !=0 && currentDate != DateTime.MinValue)
            {
                ViewBag.CurrentCustId = currentCustId;
                ViewBag.CurrentServId = currentServId;
                ViewBag.CurrentReqDate = currentDate;
                var startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
                var endDate = startDate.AddDays(1).AddTicks(-1);
               

                List<CATLogsOfService> cAtDetailList = db.CATLogsOfService.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == currentCustId && p.ServiceID == currentServId).OrderBy(d => d.DateOfRequest).ToList();
                if (cAtDetailList.Count == 0)
                {
                    cAtDetailList = db.CATLogsOfService.Take(999).ToList();
                }
                return View(cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

            }
            else{ 
            if (searchText == null || insertDateFrom ==null || insertDateTo == null) { 
                searchText = currentFilter;
                insertDateFrom = currentFrom;
                insertDateTo = currentTo;
            }
            else
                page = 1;


            bool datCondition = false;
            bool textCondition = false;
            int searchID;
            List<CATLogsOfService> cAtDetailList = new List<CATLogsOfService>();
            int.TryParse(searchText, out searchID);
            if (insertDateFrom != null) datCondition = true;
            if (searchText != null) textCondition = true;


            
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
                cAtDetailList = db.CATLogsOfService.Where(p => p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.CustomerID == searchID).Take(999).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && datCondition)
            {
                cAtDetailList = db.CATLogsOfService.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText) || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).ToList();
            }

            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.OrderByDescending(d => d.DateOfRequest).Take(999).ToList();
            }
            return View(cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
            }
        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText)
        {
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;
            ViewBag.CurrentFilter = searchText;
           
            bool datCondition = false;
            bool textCondition = false;
            int searchID;
            List<CATLogsOfService> cAtDetailList = new List<CATLogsOfService>();
            int.TryParse(searchText, out searchID);
            if (!insertDateFrom.IsNullOrWhiteSpace()) datCondition = true;
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;
           

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
                cAtDetailList = db.CATLogsOfService.Where(p => p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.CustomerID == searchID).Take(999).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && datCondition)
            {
                cAtDetailList = db.CATLogsOfService.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.BatchID == searchID || p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText) || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).ToList();
            }

            if (cAtDetailList.Count == 0)
            {
                cAtDetailList = db.CATLogsOfService.OrderByDescending(d => d.DateOfRequest).Take(999).ToList();
            }
            return View("DetailData", cAtDetailList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }

        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
           
            int pageNumber = (page ?? 1);

            var startDate = new DateTime(reqDate.Year, reqDate.Month, reqDate.Day);
            var endDate = startDate.AddDays(1).AddTicks(-1);
            ViewBag.CurrentCustId = custId;
            ViewBag.CurrentServId = servId;
            ViewBag.CurrentReqDate = reqDate;
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
