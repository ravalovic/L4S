using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using Microsoft.Ajax.Utilities;
using WebPortal.Models;

namespace WebPortal.Views
{
    public class CustomerDailyDatasController : Controller
    {
        
        private readonly L4SDb _db = new L4SDb();
        private List<view_DailyData> _dataList = new List<view_DailyData>();
        
        // GET: CATCustomerDailyDatas
        public ActionResult CustomerDaily(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime? currentDate)
        {
            var dbAccess = _db.view_DailyData;
            var pager = new Pager(dbAccess.Count(), page);
            var toSkip = (pager.CurrentPage - 1) * pager.PageSize;
            var toTake = pager.PageSize;
            var model = _dataList;
            

            if (currentCustId != 0 && currentServId != 0 && currentDate.HasValue)
            {
                ViewBag.CurrentCustId = currentCustId;
                ViewBag.CurrentServId = currentServId;
                ViewBag.CurrentReqDate = currentDate;
                var startDate = new DateTime(currentDate.Value.Year, currentDate.Value.Month, currentDate.Value.Day);
                var endDate = startDate.AddDays(1).AddTicks(-1);
                _dataList = dbAccess.Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate && p.CustomerID == currentCustId && p.ServiceID == currentServId).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                if (_dataList.Count == 0)
                {
                    _dataList = dbAccess.OrderByDescending(p=>p.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                return View(_dataList.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));
                

            }
            else
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
               
                int.TryParse(searchText, out  int searchId);
                if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
                if (searchText != null) textCondition = true;

               DateTime.TryParse(insertDateFrom, out DateTime fromDate);
                if (!DateTime.TryParse(insertDateTo, out DateTime toDate))
                {
                    toDate = DateTime.Now;
                }
                if (fromDate == toDate) toDate = toDate.AddDays(1);

                if (datCondition && !textCondition)
                {
                    model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
                    _dataList = model.Skip(toSkip).Take(toTake).ToList();
                    //_dataList = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                if (textCondition && !datCondition)
                {
                    if (searchId != 0)
                    {
                        model = dbAccess.Where(p => p.CustomerID == searchId || p.ServiceID == searchId).OrderByDescending(d => d.DateOfRequest).ToList();
                        _dataList = model.Skip(toSkip).Take(toTake).ToList();
                        //_dataList = dbAccess.Where(p => p.CustomerID == searchId || p.ServiceID == searchId).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                    }
                    else
                    {
                        model = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).ToList();
                        _dataList = model.Skip(toSkip).Take(toTake).ToList();
                        //_dataList = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                    }
                }
                if (textCondition && datCondition)
                {
                    if (searchId != 0)
                    {
                        model = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.ServiceID == searchId || p.CustomerID == searchId)).OrderByDescending(d => d.DateOfRequest).ToList();
                        _dataList = model.Skip(toSkip).Take(toTake).ToList();
                        //_dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.ServiceID == searchId || p.CustomerID == searchId)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                    }
                    else
                    {
                        model =  dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).ToList();
                        _dataList = model.Skip(toSkip).Take(toTake).ToList();
                        //_dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText)||p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                    }
                }

                if (_dataList.Count == 0)
                {
                    model = dbAccess.OrderByDescending(d => d.DateOfRequest).ToList();
                    _dataList = model.Skip(toSkip).Take(toTake).ToList();
                    //_dataList = dbAccess.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }

                var pageList = new StaticPagedList<view_DailyData>(_dataList, pager.CurrentPage, pager.PageSize, pager.TotalItems);
                ViewBag.PageList = pageList;
                //return View(model.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));
                return View();
            }
        }

        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.view_DailyData;
            var pager = new Pager(dbAccess.Count(), page);
            var toSkip = (pager.CurrentPage - 1) * pager.PageSize;
            var toTake = pager.PageSize;
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
                _dataList = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            if (textCondition && !datCondition)
            {
                if (searchId != 0)
                {
                    _dataList = dbAccess.Where(p => p.CustomerID == searchId || p.ServiceID == searchId).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    _dataList = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText)||p.ServiceCode.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
            }
            if (textCondition && datCondition)
            {
                if (searchId != 0)
                {
                    _dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerID == searchId || p.ServiceID == searchId)).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
                else
                {
                    _dataList = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
                }
            }

            if (_dataList.Count == 0)
            {
                _dataList = dbAccess.OrderByDescending(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            return View("CustomerDaily", _dataList.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));

        }

        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
            var dbAccess = _db.view_DailyData;
            var pager = new Pager(dbAccess.Count(), page);
            var toSkip = (pager.CurrentPage - 1) * pager.PageSize;
            var toTake = pager.PageSize;

            var startDate = new DateTime(reqDate.Year, reqDate.Month, reqDate.Day);
            //var endDate = startDate.AddDays(1);//.AddTicks(-1);
            ViewBag.CurrentCustId = custId;
            ViewBag.CurrentServId = servId;
            ViewBag.CurrentReqDate = reqDate;
            _dataList = dbAccess.Where(p => p.DateOfRequest >= startDate.Date &&  p.CustomerID == custId && p.ServiceID == servId).OrderBy(d => d.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            if (_dataList.Count == 0)
            {
                _dataList = dbAccess.OrderByDescending(p=>p.DateOfRequest).Skip(toSkip).Take(toTake).ToList();
            }
            return View("CustomerDaily", _dataList.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));

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
