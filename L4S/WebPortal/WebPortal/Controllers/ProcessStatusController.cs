using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using WebPortal.Common;
using Microsoft.Ajax.Utilities;

namespace WebPortal.Controllers
{

    public class ProcessStatusController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<CATProcessStatus> _dataList;
        private List<CATProcessStatus> _model;
        private Pager _pager;
        // GET: ProcessStatus
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

            // set actual filter to ViewBag
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
            var pageList = new StaticPagedList<CATProcessStatus>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }
        private List<CATProcessStatus> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter)
        {
            filter = true;
            var dbAccess = _db.CATProcessStatus;
            List<CATProcessStatus> model = new List<CATProcessStatus>();

            if (datCon && !txtCon)
            {
                model = dbAccess.Where(p => p.TCInsertTime.Value >= fromDate && p.TCInsertTime.Value <= toDate)
                    .OrderBy(d => d.TCInsertTime).ToList();

            }
            if (txtCon && !datCon)
            {
                model = dbAccess
                    .Where(p => p.BatchID.ToUpper().Contains(search.ToUpper()) ||
                                p.StepName.ToUpper().Contains(search.ToUpper()) ||
                                p.BatchID.ToUpper().Contains(search.ToUpper()))
                    .OrderByDescending(d => d.TCInsertTime.Value).ToList();

            }
            if (txtCon && datCon)
            {
                model = dbAccess
                    .Where(p => (p.TCInsertTime.Value >= fromDate && p.TCInsertTime.Value <= toDate) &&
                                (
                                    p.BatchID.ToUpper().Contains(search.ToUpper()) ||
                                    p.StepName.ToUpper().Contains(search.ToUpper()) ||
                                    p.BatchID.ToUpper().Contains(search.ToUpper())))
                    .OrderByDescending(d => d.TCInsertTime).ToList();

            }

            if (model.Count == 0)
            {
                model = dbAccess.OrderByDescending(d => d.TCInsertTime).ToList();
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
