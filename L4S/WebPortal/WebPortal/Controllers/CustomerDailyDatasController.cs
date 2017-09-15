using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoddleReport;
using DoddleReport.Writers;
using WebPortal.DataContexts;
using PagedList;
using Microsoft.Ajax.Utilities;
using WebPortal.Common;
using static WebPortal.Common.Helper;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
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

            _model = ApplyFilter(searchText, searchId, fromDate, toDate, textCondition, datCondition, out bool aFilter, out Statistics statistics);
            if (!aFilter)
            {
                ViewBag.CurrentFilter = string.Empty;
                ViewBag.CurrentFrom = string.Empty;
                ViewBag.CurrentTo = string.Empty;
            }

            ViewBag.CustomerCount = statistics.CustomerCount;
            ViewBag.ServiceCount = statistics.ServiceCount;
            ViewBag.RequestCount = statistics.RequestCount;
            ViewBag.ReceivedBytes = statistics.ReceivedBytes;
            ViewBag.SessionDuration = statistics.SessionDuration;

            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_DailyData>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //ViewBag.PageList = pageList;
            //return View(model.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index", pageList);
        }
        public ActionResult Report(string extension, int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
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

            _model = ApplyFilter(searchText, searchId, fromDate, toDate, textCondition, datCondition, out bool aFilter, out Statistics statistics);
            if (!aFilter)
            {
                ViewBag.CurrentFilter = string.Empty;
                ViewBag.CurrentFrom = string.Empty;
                ViewBag.CurrentTo = string.Empty;
            }

            ViewBag.CustomerCount = statistics.CustomerCount;
            ViewBag.ServiceCount = statistics.ServiceCount;
            ViewBag.RequestCount = statistics.RequestCount;
            ViewBag.ReceivedBytes = statistics.ReceivedBytes;
            ViewBag.SessionDuration = statistics.SessionDuration;

            string statisticsString = string.Format(@"
{0} : {1}  {2} : {3}   {4} : {5}   {6} : {7}   {8} : {9}
"
                , Resources.Labels.Statistics_CustomerCount, statistics.CustomerCount,
                Resources.Labels.Statistics_ServiceCount, statistics.ServiceCount,
                Resources.Labels.Statistics_RequestCount, statistics.RequestCount,
                Resources.Labels.Statistics_ReceivedBytes, statistics.ReceivedBytes,
                Resources.Labels.Statistics_SessionDuration, statistics.SessionDuration);

            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            #region ********************* Report  *******************************
            var reportName = "DennySumar_" + DateTime.Now.ToString("MMyyyy");

            var reportFromDate = _model.Min(p => p.DateOfRequest).Value.ToString("dd.MM.yyyy");
            var reportToDate = _model.Max(p => p.DateOfRequest).Value.ToString("dd.MM.yyyy");
            // Create the report and turn our query into a ReportSource
            var report = new Report(_model.ToReportSource());
            if (extension.Equals("csv"))
            {
                string delimiter = ";";
                var confGeneralSettings = _db.CONFGeneralSettings.FirstOrDefault(p => p.ParamName.Equals("CSVDelimiter"));
                if (confGeneralSettings != null)
                {
                    delimiter = confGeneralSettings.ParamValue;
                }
                DelimitedTextReportWriter.DefaultDelimiter = delimiter;
            }
            if (extension.Equals("pdf"))
            {
                var confGeneralSettings = _db.CONFGeneralSettings.FirstOrDefault(p => p.ParamName.Equals("ReportOrientation"));
                if (confGeneralSettings != null)
                {
                    if (confGeneralSettings.ParamValue.Equals("Landscape"))
                    {
                        report.RenderHints.Orientation = ReportOrientation.Landscape;
                    }
                }
            }
            //Header report
            report.TextFields.Title = "Denné sumárne údaje";
            report.TextFields.SubTitle = "Obdobie od: " + reportFromDate + " do: " + reportToDate;
            report.TextFields.Header = statisticsString;
            //report.TextFields.Footer = "Copyright 2017 (c) BlueZ, s.r.o.";
            report.TextFields.Footer = string.Format(@"
Report vytvorený: {0}  Počet záznamov: {1} (c) copyright VUGK", DateTime.Now, _model.Count);

            // Render hints allow you to pass additional hints to the reports as they are being rendered
            report.RenderHints.BooleanCheckboxes = true;
            report.RenderHints.BooleansAsYesNo = true;
            
            
            
            

            //Data fields
            //report.DataFields[nameof(view_DailyData.DateOfRequest)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.CustomerID)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.CustomerIdentification)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.CustomerName)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.ServiceID)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.ServiceCode)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.ServiceDescription)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.NumberOfRequest)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.ReceivedBytes)].Hidden = true;
            //report.DataFields[nameof(view_DailyData.RequestedTime)].Hidden = true;
            report.DataFields[nameof(view_DailyData.TCActive)].Hidden = true;

            return new ReportResult(report) { FileName = reportName };
            #endregion
        }
        private List<view_DailyData> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter, out Statistics stats)
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
            stats = new Helper.Statistics
            {
                ReceivedBytes = model.Sum(p => p.ReceivedBytes),
                CustomerCount = model.Select(p => p.CustomerID).Distinct().Count(),
                RequestCount = model.Sum(p => p.NumberOfRequest),
                ServiceCount = model.Select(p => p.ServiceID).Distinct().Count(),
                SessionDuration = model.Sum(p => p.RequestedTime)
            };
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
