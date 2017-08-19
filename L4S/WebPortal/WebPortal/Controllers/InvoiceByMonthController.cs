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
    public class InvoiceByMonthController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<view_InvoiceByMonth> _dataList;
        private List<view_InvoiceByMonth> _model;
        private Pager _pager;

        // GET: InvoiceByMonth
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.view_InvoiceByMonth;
            if (searchText.IsNullOrWhiteSpace())
            {
                searchText = currentFilter;
            }
            if (insertDateFrom.IsNullOrWhiteSpace())
            {
                insertDateFrom = currentFrom;
            }
            if (insertDateTo.IsNullOrWhiteSpace())
            {
                insertDateTo = currentTo;
            }

            // set actual filter to ViewBag
            ViewBag.CurrentFilter = searchText;
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;

            if (searchText.IsNullOrWhiteSpace() && insertDateFrom.IsNullOrWhiteSpace() &&
                insertDateTo.IsNullOrWhiteSpace())
            {
                _model = dbAccess.OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ToList();
                if (_model.FirstOrDefault() != null)
                {
                    insertDateFrom = _model.FirstOrDefault().DateOfRequest.ToString("dd.MM.yyyy");
                    ViewBag.CurrentFrom = _model.FirstOrDefault().DateOfRequest.ToString("MM.yyyy");
                    ViewBag.CurrentTo = ViewBag.CurrentFrom;
                }
                else
                {
                    insertDateFrom = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("dd.MM.yyyy");
                    ViewBag.CurrentFrom = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("MM.yyyy");
                    ViewBag.CurrentTo = ViewBag.CurrentFrom;
                }
                insertDateTo = insertDateFrom;
            }

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
            if (fromDate == toDate) toDate = toDate.AddDays(1).AddTicks(-1);

            if (datCondition && !textCondition)
            {
                _model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate)
                    .OrderBy(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.CustomerID == searchId ||p.ServiceID == searchId ||
                                p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                p.ServiceCode.ToUpper().Contains(searchText.ToUpper()) ||
                                p.CustomerServicename.ToUpper().Contains(searchText.ToUpper()) ||
                                p.CustomerServiceCode.ToUpper().Contains(searchText.ToUpper()) 
                                )
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p=>p.CustomerID).ThenBy(p=>p.ServiceID).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                (p.CustomerID == searchId || p.ServiceID == searchId ||
                                 p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.ServiceCode.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.CustomerServicename.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.CustomerServiceCode.ToUpper().Contains(searchText.ToUpper())))
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.DateOfRequest)
                    .ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_InvoiceByMonth>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }
        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
            var dbAccess = _db.view_InvoiceByMonth;
            var startDate = new DateTime(reqDate.Year, reqDate.Month, reqDate.Day);
            var endDate = startDate.AddMonths(1).AddTicks(-1);
            ViewBag.CurrentCustId = custId;
            ViewBag.CurrentServId = servId;
            ViewBag.CurrentReqDate = reqDate;
            _model = dbAccess
                .Where(p => p.DateOfRequest >= startDate.Date && p.DateOfRequest <= endDate && p.CustomerID == custId )
                .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.ServiceID).ToList();
            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(p => p.DateOfRequest).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_InvoiceByMonth>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
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
