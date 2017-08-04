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

namespace WebPortal.Views
{
    public class CustomerDailyDatasController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: CATCustomerDailyDatas
        public ActionResult CustomerDaily(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<view_DailyData> cAtDailyList = db.view_DailyData.OrderByDescending(d => d.DateOfRequest).ToList();
            return View(cAtDailyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
           
        }

        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText)
        {
            bool datCondition = false;
            bool textCondition = false;
            int searchID;
            List<view_DailyData> cAtDailyList = new List<view_DailyData>();
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
                cAtDailyList = db.view_DailyData.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && !datCondition)
            {
                cAtDailyList = db.view_DailyData.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.CustomerID == searchID).OrderByDescending(d => d.DateOfRequest).ToList();
            }
            if (textCondition && datCondition)
            {
                cAtDailyList = db.view_DailyData.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.CustomerID == searchID)).OrderByDescending(d => d.DateOfRequest).ToList();
            }

            if (cAtDailyList.Count == 0)
            {
                cAtDailyList = db.view_DailyData.OrderByDescending(d => d.DateOfRequest).ToList();
            }
            return View("CustomerDaily", cAtDailyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }

        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
            int pageNumber = (page ?? 1);

            var startDate = new DateTime(reqDate.Year, reqDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);


            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            
            List<view_DailyData> cAtDailyList = db.view_DailyData.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == custId && p.ServiceID == servId).OrderBy(d => d.DateOfRequest).ToList();
            if (cAtDailyList.Count == 0)
            {
                cAtDailyList = db.view_DailyData.ToList();
            }
            return View("CustomerDaily", cAtDailyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
