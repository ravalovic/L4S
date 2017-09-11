using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using WebPortal.Common;
using Microsoft.Ajax.Utilities;

namespace WebPortal.Controllers
{
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class UnknownServicesController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<CATUnknownService> _dataList;
        private List<CATUnknownService> _model;
        private Pager _pager;
        // GET: UnknownServices
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.CATUnknownService;
            int searchId;
            DateTime fromDate;
            DateTime toDate;
            bool datCondition = false;
            bool textCondition = false;
            Helper.SetUpFilterValues(ref searchText, ref insertDateFrom, ref insertDateTo, currentFilter, currentFrom, currentTo, out searchId, out fromDate, out toDate, page);
            if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;

            // set actual filter to ViewBag
            ViewBag.CurrentFilter = searchText;
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;

           
            if (datCondition && !textCondition)
            {
                _model = dbAccess.Where(p => p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate)
                    .OrderBy(d => d.TCInsertTime).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.BatchID == searchId||
                                p.RequestedURL.ToUpper().Contains(searchText.ToUpper()) ||
                                p.UserIPAddress.ToUpper().Contains(searchText.ToUpper()) ||
                                p.UserAgent.ToUpper().Contains(searchText.ToUpper()))
                    .OrderByDescending(d => d.DateOfRequest).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate)&&
                                (p.BatchID == searchId ||
                                p.RequestedURL.ToUpper().Contains(searchText.ToUpper()) ||
                                p.UserIPAddress.ToUpper().Contains(searchText.ToUpper()) ||
                                p.UserAgent.ToUpper().Contains(searchText.ToUpper())))
                                 .OrderByDescending(d => d.DateOfRequest).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.TCInsertTime).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<CATUnknownService>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }

        // GET: UnknownServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATUnknownService cAtUnknownService = _db.CATUnknownService.Find(id);
            if (cAtUnknownService == null)
            {
                return HttpNotFound();
            }
            return View(cAtUnknownService);
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
