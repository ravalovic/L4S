using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Common;
using PagedList;
using Microsoft.Ajax.Utilities;
using DoddleReport;
using DoddleReport.Writers;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class InvoiceByDayController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<view_InvoiceByDay> _dataList;
        private List<view_InvoiceByDay> _model;
        private Pager _pager;

        // GET: InvoiceByDay
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime? currentDate)
        {
            int searchId;
            DateTime fromDate;
            DateTime toDate;
            bool datCondition = false;
            bool textCondition = false;
            var dbAccess = _db.view_InvoiceByDay;
            var lastPeriod = DateTime.Today;
            if (dbAccess.Any())
            {
                lastPeriod = dbAccess.Max(p => p.DateOfRequest);
            }
            if (searchText.IsNullOrWhiteSpace() && insertDateFrom.IsNullOrWhiteSpace() &&
                insertDateTo.IsNullOrWhiteSpace() && currentFilter.IsNullOrWhiteSpace() &&
                currentFrom.IsNullOrWhiteSpace() && currentTo.IsNullOrWhiteSpace())
            {
                insertDateFrom = lastPeriod.ToString("dd.MM.yyyy");
            }

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
            var pageList = new StaticPagedList<view_InvoiceByDay>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //ViewBag.PageList = pageList;
            //return View(model.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index", pageList);
        }
        
        public ActionResult Report(string extension, int? page, string insertDateFrom, string insertDateTo,
            string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId,
            int? currentServId, DateTime? currentDate)

        {
            int searchId;
            DateTime fromDate;
            DateTime toDate;
            bool datCondition = false;
            bool textCondition = false;
            var dbAccess = _db.view_InvoiceByDay;
            var lastPeriod = dbAccess.Max(p => p.DateOfRequest);
            if (searchText.IsNullOrWhiteSpace() && insertDateFrom.IsNullOrWhiteSpace() &&
                insertDateTo.IsNullOrWhiteSpace() && currentFilter.IsNullOrWhiteSpace() &&
                currentFrom.IsNullOrWhiteSpace() && currentTo.IsNullOrWhiteSpace())
            {
                insertDateFrom = lastPeriod.ToString("dd.MM.yyyy");
            }

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

            #region ********************* Report  *******************************

            var reportName = "FakturacneUdajeDenne_" + DateTime.Now.ToString("MMyyyy");

            if (extension.Equals("csv"))
            {
                string delimiter = ";";
                var confGeneralSettings =
                    _db.CONFGeneralSettings.FirstOrDefault(p => p.ParamName.Equals("CSVDelimiter"));
                if (confGeneralSettings != null)
                {
                    delimiter = confGeneralSettings.ParamValue;
                }
                DelimitedTextReportWriter.DefaultDelimiter = delimiter;
            }
            var reportFromDate = _model.Min(p => p.DateOfRequest).ToString("dd.MM.yyyy");
            var reportToDate = _model.Max(p => p.DateOfRequest).ToString("dd.MM.yyyy");
            // Create the report and turn our query into a ReportSource
            var report = new Report(_model.ToReportSource());

            //Header report
            report.TextFields.Title = "Denné detailné fakturačné údaje";
            report.TextFields.SubTitle = "Obdobie od: " + reportFromDate + " do: " + reportToDate;
            //report.TextFields.Footer = "Copyright 2017 (c) BlueZ, s.r.o.";
            report.TextFields.Header = string.Format(@"
                Report vytvorený: {0}
                Počet záznamov: {1}
                ", DateTime.Now, _model.Count);

            // Render hints allow you to pass additional hints to the reports as they are being rendered
            report.RenderHints.BooleanCheckboxes = true;
            report.RenderHints.BooleansAsYesNo = true;

            //Data fields
            report.DataFields[nameof(view_InvoiceByMonth.ID)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.DateOfRequest)].Hidden = true;
            report.DataFields[nameof(view_InvoiceByMonth.CustomerID)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.CustomerIdentification)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.CustomerName)].Hidden = true;
            report.DataFields[nameof(view_InvoiceByMonth.ServiceID)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.ServiceCode)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.ServiceDescription)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.NumberOfRequest)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.ReceivedBytes)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.RequestedTime)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.CustomerServiceCode)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.CustomerServicename)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.UnitPrice)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.MeasureOfUnits)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.BasicPriceWithoutVAT)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.VAT)].Hidden = true;
            //report.DataFields[nameof(view_InvoiceByMonth.BasicPriceWithVAT)].Hidden = true;
            report.DataFields[nameof(view_InvoiceByMonth.TCActive)].Hidden = true;

            return new ReportResult(report) {FileName = reportName};
        }

        #endregion
        private List<view_InvoiceByDay> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter)
        {
            filter = true;
            var dbAccess = _db.view_InvoiceByDay;
            List<view_InvoiceByDay> model = new List<view_InvoiceByDay>();
            if (datCon && !txtCon)
            {
                model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate)
                    .OrderBy(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ToList();

            }
            if (txtCon && !datCon)
            {
                model = dbAccess
                    .Where(p => p.CustomerID == searchId ||
                                p.CustomerName.ToUpper().Contains(search.ToUpper()) ||
                                p.CustomerIdentification.ToUpper().Contains(search.ToUpper())
                    )
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ToList();

            }
            if (txtCon && datCon)
            {
                model = dbAccess
                    .Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                (p.CustomerID == searchId ||
                                 p.CustomerName.ToUpper().Contains(search.ToUpper()) ||
                                 p.CustomerIdentification.ToUpper().Contains(search.ToUpper())))
                    .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ToList();

            }

            if (model.Count == 0)
            {
                model = dbAccess.OrderByDescending(d => d.DateOfRequest)
                    .ThenBy(p => p.CustomerID).ToList();
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
