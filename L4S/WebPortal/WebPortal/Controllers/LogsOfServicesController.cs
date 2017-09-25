using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebPortal.DataContexts;
using WebPortal.Common;
using PagedList;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class LogsOfServicesController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<CATLogsOfService> _dataList;
        private List<CATLogsOfService> _model;
        private Pager _pager;


        // GET: LogsOfServices
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)

        {
            int sid = 0;
            int cid = 0;
            int searchId;
            var pattern1 = @"cid=\d+";
            var pattern2 = @"sid=\d+";
            DateTime fromDate;
            DateTime toDate;
            bool datCondition = false;
            bool textCondition = false;
            Helper.SetUpFilterValues(ref searchText, ref insertDateFrom, ref insertDateTo, currentFilter, currentFrom, currentTo, out searchId, out fromDate, out toDate, page);
            if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
            if (!searchText.IsNullOrWhiteSpace()) textCondition = true;
            if (!searchText.IsNullOrWhiteSpace())
            {
                cid = Int32.Parse(Regex.Match(Regex.Match(searchText, pattern1).Value, @"\d+").Value);
                sid = Int32.Parse(Regex.Match(Regex.Match(searchText, pattern2).Value, @"\d+").Value);
            }
            if (cid !=0 && sid != 0) { datCondition = false;
                textCondition = false; 
            }

            // set actual filter to VieBag
            ViewBag.CurrentFilter = searchText;
            ViewBag.CurrentFrom = insertDateFrom;
            ViewBag.CurrentTo = insertDateTo;

            _model = ApplyFilter(searchText, searchId, fromDate, toDate, textCondition, datCondition, out bool aFilter, cid, sid);
            if (!aFilter)
            {
                ViewBag.CurrentFilter = string.Empty;
                ViewBag.CurrentFrom = string.Empty;
                ViewBag.CurrentTo = string.Empty;
            }

            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<CATLogsOfService>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }


       private List<CATLogsOfService> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter, int custId, int servId)
        {
            filter = true;
            int takeLimit = 9999;
            var dbAccess = _db.CATLogsOfService;
            List<CATLogsOfService> model = new List<CATLogsOfService>();

            if (datCon && !txtCon)
            {
                if (dbAccess.Count(p => p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate) > takeLimit)
                {
                    model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate)
                        .OrderByDescending(d => d.DateOfRequest).Take(takeLimit).ToList();
                }
                else
                {
                    model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate)
                        .OrderByDescending(d => d.DateOfRequest).ToList();
                }
            }

            if (txtCon && !datCon)
            {
                if (dbAccess.Count(p => p.BatchID == searchId ||
                                        p.CustomerID == searchId ||
                                        p.RequestedURL.Contains(search) ||
                                        p.UserAgent.Contains(search) ||
                                        p.UserIPAddress.Contains(search))
                    > takeLimit)
                {
                    model = dbAccess
                        .Where(p => p.BatchID == searchId ||
                                    p.CustomerID == searchId ||
                                    p.RequestedURL.Contains(search) ||
                                    p.UserAgent.Contains(search) ||
                                    p.UserIPAddress.Contains(search))
                        .OrderByDescending(d => d.DateOfRequest).Take(takeLimit).ToList();
                }
                else
                {
                    model = dbAccess
                        .Where(p => p.BatchID == searchId ||
                                    p.CustomerID == searchId ||
                                    p.RequestedURL.Contains(search) ||
                                    p.UserAgent.Contains(search) ||
                                    p.UserIPAddress.Contains(search))
                        .OrderByDescending(d => d.DateOfRequest).ToList();
                }
            }

            if (txtCon && datCon)
            {
                if (dbAccess.Count(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                                               (p.BatchID == searchId ||
                                                               p.CustomerID == searchId ||
                                                               p.RequestedURL.Contains(search) ||
                                                               p.UserAgent.Contains(search) ||
                                                               p.UserIPAddress.Contains(search))) > takeLimit)
                {
                    model = dbAccess
                        .Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                    (p.BatchID == searchId ||
                                     p.CustomerID == searchId ||
                                     p.RequestedURL.Contains(search) ||
                                     p.UserAgent.Contains(search) ||
                                     p.UserIPAddress.Contains(search)))
                        .OrderByDescending(d => d.DateOfRequest).Take(takeLimit)
                        .ToList();
                }
                else
                {
                    model = dbAccess
                        .Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                    (p.BatchID == searchId ||
                                     p.CustomerID == searchId ||
                                     p.RequestedURL.Contains(search) ||
                                     p.UserAgent.Contains(search) ||
                                     p.UserIPAddress.Contains(search)))
                        .OrderByDescending(d => d.DateOfRequest)
                        .ToList();
                }
            }

            if (!datCon && !txtCon)
            {
                toDate = toDate.AddDays(1).AddTicks(-1);

                model = dbAccess.Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                              (p.CustomerID == custId &&
                                               p.ServiceID == servId
                                               ))
                    .OrderByDescending(d => d.DateOfRequest)
                    .ToList();
            }

            if (model.Count == 0)
            {
                if (dbAccess.Count() > takeLimit)
                {
                    model = dbAccess.OrderByDescending(d => d.DateOfRequest).Take(takeLimit).ToList();
                }
                else
                {
                    model = dbAccess.OrderByDescending(d => d.DateOfRequest).ToList();
                }
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
