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
    public class CustomerMonthlyDatasController : Controller
    {
        private const int PageSize = 30;
        private const int ToTake = 900;
        private readonly L4SDb _db = new L4SDb();
        private List<view_MonthlyData> _dataList = new List<view_MonthlyData>();
       
        // GET: CATCustomerMonthlyDatas
        public ActionResult CustomerMonthly(int? page)
        {
            var dbAccess = _db.view_MonthlyData;
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
            }
            _dataList = dbAccess.Where(p=>p.TCActive!=99).OrderByDescending(l=>l.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
            return View(_dataList.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));

        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.view_MonthlyData;
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
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
            
            int.TryParse(searchText, out int searchId);
            if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;


            
            DateTime.TryParse(insertDateFrom, out DateTime fromDate);
            if (!DateTime.TryParse(insertDateTo, out DateTime toDate))
            {
                toDate = DateTime.Now;
            }
            if (fromDate == toDate) toDate = toDate.AddDays(1);

            if (datCondition && !textCondition)
            {
                _dataList = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
            }
            if (textCondition && !datCondition)
            {
                if (searchId != 0)
                {
                    _dataList = dbAccess.Where(p => p.CustomerID == searchId || p.ServiceID == searchId).Take(ToTake).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
                }
                else
                {
                    _dataList = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText)|| p.ServiceCode.Contains(searchText)).Take(ToTake).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
                }
            }
            if (textCondition && datCondition)
            {
                if (searchId != 0)
                {
                    _dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerID == searchId || p.ServiceID == searchId)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
                }
                else
                {
                    _dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
                }


            }

            if (_dataList.Count == 0)
            {
                _dataList = _db.view_MonthlyData.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(ToTake).ToList();
            }
            return View("CustomerMonthly", _dataList.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
