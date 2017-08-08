using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Models
{
    public class CustomerMonthlyDatasController : Controller
    {
        private const int pageSize = 30;
        private const int toTake = 999;
        private static readonly L4SDb db = new L4SDb();
        private List<view_MonthlyData> dataList = new List<view_MonthlyData>();
        private readonly DbSet<view_MonthlyData> dbAccess = db.view_MonthlyData;
        // GET: CATCustomerMonthlyDatas
        public ActionResult CustomerMonthly(int? page)
        {
            List<view_MonthlyData> dataList = new List<view_MonthlyData>();
            var dbAccess = db.view_MonthlyData;
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            dataList = db.view_MonthlyData.Where(p=>p.TCActive!=99).OrderByDescending(l=>l.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            return View(dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
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
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;


            
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
                    dataList = dbAccess.Where(p => p.CustomerID == searchID || p.ServiceID == searchID).Take(toTake).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText)|| p.ServiceCode.Contains(searchText)).Take(toTake).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
            }
            if (textCondition && datCondition)
            {
                if (searchID != 0)
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerID == searchID || p.ServiceID == searchID)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }


            }

            if (dataList.Count == 0)
            {
                dataList = db.view_MonthlyData.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            return View("CustomerMonthly", dataList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
