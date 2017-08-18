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
            var dbAccess = _db.CATProcessStatus;
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
                _model = dbAccess.Where(p => p.TCInsertTime >= fromDate && p.TCInsertTime <= toDate)
                    .OrderBy(d => d.TCInsertTime).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.BatchID.ToUpper().Contains(searchText.ToUpper()) ||
                                p.StepName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.BatchID.ToUpper().Contains(searchText.ToUpper()))
                    .OrderByDescending(d => d.TCInsertTime).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.BatchID.ToUpper().Contains(searchText.ToUpper()) || 
                                p.TCInsertTime >= fromDate && p.TCInsertTime <= toDate) &&
                                (p.StepName.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.BatchID.ToUpper().Contains(searchText.ToUpper()))).OrderByDescending(d => d.TCInsertTime).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.TCInsertTime).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<CATProcessStatus>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
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
