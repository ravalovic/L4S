using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebPortal.DataContexts;
using WebPortal.Models;
using PagedList;

namespace WebPortal.Controllers
{
    public class CustomerMonthlyDatasController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<view_MonthlyData> _dataList;
        private List<view_MonthlyData> _model;
        private Pager _pager;

        // GET: CATCustomerMonthlyDatas
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime? currentDate)
        {
            var dbAccess = _db.view_MonthlyData;
            if (currentCustId != 0 && currentServId != 0 && currentDate.HasValue)
            {
                ViewBag.CurrentCustId = currentCustId;
                ViewBag.CurrentServId = currentServId;
                ViewBag.CurrentReqDate = currentDate;
                var startDate = new DateTime(currentDate.Value.Year, currentDate.Value.Month, currentDate.Value.Day);
                var endDate = startDate.AddDays(1).AddTicks(-1);
                _model = dbAccess
                    .Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate &&
                                p.CustomerID == currentCustId && p.ServiceID == currentServId)
                    .OrderBy(d => d.DateOfRequest).ToList();
                //return View(_dataList.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));
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

                int.TryParse(searchText, out int searchId);
                if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
                if (searchText != null) textCondition = true;

                DateTime.TryParse(insertDateFrom, out DateTime fromDate);
                if (!DateTime.TryParse(insertDateTo, out DateTime toDate))
                {
                    toDate = DateTime.Now;
                }
                if (fromDate == toDate) toDate = toDate.AddDays(1).AddTicks(-1);

                if (datCondition && !textCondition)
                {
                    _model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
                }
                if (textCondition && !datCondition)
                {
                    if (searchId != 0)
                    {
                        _model = dbAccess.Where(p => p.CustomerID == searchId || p.ServiceID == searchId).OrderByDescending(d => d.DateOfRequest).ToList();
                    }
                    else
                    {
                        _model = dbAccess.Where(p => p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText)).OrderByDescending(d => d.DateOfRequest).ToList();
                    }
                }
                if (textCondition && datCondition)
                {
                    if (searchId != 0)
                    {
                        _model = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.ServiceID == searchId || p.CustomerID == searchId)).OrderByDescending(d => d.DateOfRequest).ToList();
                    }
                    else
                    {
                        _model = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) && (p.CustomerIdentification.Contains(searchText) || p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))).OrderByDescending(d => d.DateOfRequest).ToList();
                    }
                }
            }
            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.DateOfRequest).ToList();
            }

            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_MonthlyData>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //ViewBag.PageList = pageList;
            //return View(model.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index", pageList);

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
