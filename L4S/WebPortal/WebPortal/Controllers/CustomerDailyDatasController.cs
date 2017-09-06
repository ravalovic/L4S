using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using Microsoft.Ajax.Utilities;
using WebPortal.Common;

namespace WebPortal.Controllers
{
    public class CustomerDailyDatasController : Controller
    {
        
        private readonly L4SDb _db = new L4SDb();
        private List<view_DailyData> _dataList;
        private List<view_DailyData> _model;
        private Pager _pager;
        

        // GET: CATCustomerDailyDatas
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            int searchId;
            DateTime fromDate;
            DateTime toDate;
            bool datCondition = false;
            bool textCondition = false;
            Helper.SetUpFilterValues(ref searchText, ref insertDateFrom, ref insertDateTo, currentFilter, currentFrom, currentTo, out searchId, out fromDate, out toDate, page);
            if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;

            // set actual filter to VieBag
            ViewBag.CurrentFilter = searchText;
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;
 
            _model = ApplyFilter(searchText, searchId, fromDate, toDate, textCondition, datCondition, out bool aFilter);
            if (!aFilter)
            {
                ViewBag.CurrentFilter = string.Empty;
                ViewBag.CurrentFrom = string.Empty;
                ViewBag.CurrentTo = string.Empty;
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_DailyData>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //ViewBag.PageList = pageList;
            //return View(model.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index",pageList);
        }

        private List<view_DailyData> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter)
        {
            filter = true;
            var dbAccess = _db.view_DailyData;
            List<view_DailyData> model = new List<view_DailyData>();

            if (datCon && !txtCon)
            {
                model = dbAccess.Where(p => p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();

            }
            if (txtCon && !datCon)
            {
                model = dbAccess
                    .Where(p => p.CustomerID == searchId || p.ServiceID == searchId ||
                                p.CustomerName.ToUpper().Contains(search.ToUpper()) ||
                                p.CustomerIdentification.ToUpper().Contains(search.ToUpper()) ||
                                p.ServiceCode.ToUpper().Contains(search.ToUpper()) ||
                                p.CustomerName.ToUpper().Contains(search.ToUpper())
                    )
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

            }
            if (txtCon && datCon)
            {
                model = dbAccess
                    .Where(p => (p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate) &&
                                (p.CustomerID == searchId || p.ServiceID == searchId ||
                                 p.CustomerName.ToUpper().Contains(search.ToUpper()) ||
                                 p.CustomerIdentification.ToUpper().Contains(search.ToUpper()) ||
                                 p.ServiceCode.ToUpper().Contains(search.ToUpper()) ||
                                 p.CustomerName.ToUpper().Contains(search.ToUpper())
                                ))
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

            }

            if (model.Count == 0)
            {
                model = dbAccess.OrderByDescending(d => d.DateOfRequest).ToList();
                filter = false;
            }
            return model;
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
