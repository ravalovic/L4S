using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Controllers
{
    public class LogsOfServicesController : Controller
    {
        private const int pageSize = 30;
        private const int toTake = 999;
        private static readonly L4SDb db = new L4SDb();
        private List<CATLogsOfService> dataList = new List<CATLogsOfService>();
        private readonly DbSet<CATLogsOfService> dbAccess = db.CATLogsOfService;

        // GET: LogsOfServices
        public ActionResult DetailData(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime? currentDate)
        {
            
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }

            if (currentCustId !=0 && currentServId !=0 && currentDate.HasValue)
            {
                ViewBag.CurrentCustId = currentCustId;
                ViewBag.CurrentServId = currentServId;
                ViewBag.CurrentReqDate = currentDate;
                var startDate = new DateTime(currentDate.Value.Year, currentDate.Value.Month, currentDate.Value.Day);
                var endDate = startDate.AddDays(1).AddTicks(-1);
                
                dataList = dbAccess.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == currentCustId && p.ServiceID == currentServId).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                if (dataList.Count == 0)
                {
                    dataList = dbAccess.Skip(toSkip).Take(toTake).ToList();
                }
                return View(dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

            }
            else{
                if (searchText.IsNullOrWhiteSpace()) { searchText = currentFilter; }
                if (insertDateFrom.IsNullOrWhiteSpace()) { insertDateFrom = currentFrom; }
                if (insertDateTo.IsNullOrWhiteSpace()) { insertDateTo = currentTo; }
                // set actual filter to VieBag
                ViewBag.CurrentFilter = searchText;
                ViewBag.CurrentFrom = insertDateFrom;
                ViewBag.CurrentTo = insertDateTo;

            bool datCondition = false;
            bool textCondition = false;
            int searchID;
           
            int.TryParse(searchText, out searchID);
                if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
                if (searchText != null) textCondition = true;
            
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
                dataList = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            if (textCondition && !datCondition)
            {
                if (searchID != 0)
                {
                    dataList = dbAccess.Where(p => p.BatchID == searchID || p.CustomerID == searchID).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                }
            if (textCondition && datCondition)
            {
                if (searchID != 0)
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.BatchID == searchID || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                }

            if (dataList.Count == 0)
            {
                dataList = dbAccess.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            return View(dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
            }
        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            if (searchText.IsNullOrWhiteSpace()) { searchText = currentFilter; }
            if (insertDateFrom.IsNullOrWhiteSpace()) { insertDateFrom = currentFrom; }
            if (insertDateTo.IsNullOrWhiteSpace()) { insertDateTo = currentTo; }

            // set actual filter to VieBag
            ViewBag.CurrentFilter = searchText;
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;

            bool datCondition = false;
            bool textCondition = false;
            int searchID;
           
            int.TryParse(searchText, out searchID);
            if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace())  datCondition = true;
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
                dataList = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            if (textCondition && !datCondition)
            {
                if (searchID != 0)
                {
                    dataList = dbAccess.Where(p => p.BatchID == searchID || p.CustomerID == searchID).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => p.RequestedURL.Contains(searchText) || p.UserAgent.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
            }
            if (textCondition && datCondition)
            {
                if (searchID != 0)
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.BatchID == searchID || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.RequestedURL.Contains(searchText)||p.UserAgent.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
            }

            if (dataList.Count == 0)
            {
                dataList = dbAccess.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            return View("DetailData", dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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

            dataList = dbAccess.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == custId && p.ServiceID == servId).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            if (dataList.Count == 0)
            {
                dataList = dbAccess.Skip(toSkip).Take(toTake).ToList();
            }
            return View("DetailData", dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
