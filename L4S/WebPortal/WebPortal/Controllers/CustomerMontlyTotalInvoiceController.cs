using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Models;
using WebPortal.Common;
using PagedList;
using Microsoft.Ajax.Utilities;
using DoddleReport;
using DoddleReport.Writers;

namespace WebPortal.Controllers
{
    public class CustomerMontlyTotalInvoiceController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<view_CustomerMontlyTotalInvoice> _dataList;
        private List<view_CustomerMontlyTotalInvoice> _model;
        private Pager _pager;

        // GET: CustomerMontlyTotalInvoice
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.view_CustomerMontlyTotalInvoice;
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
                _model = dbAccess.OrderByDescending(d => d.StartBillingPeriod).ThenBy(p => p.CustomerID).ToList();
                if (_model.FirstOrDefault() != null) { 
                    insertDateFrom = _model.FirstOrDefault().StartBillingPeriod.ToString("dd.MM.yyyy");
                    ViewBag.CurrentFrom = _model.FirstOrDefault().StartBillingPeriod.ToString("MM.yyyy");
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
            if (fromDate == toDate) toDate = toDate.AddMonths(1).AddTicks(-1);

            if (datCondition && !textCondition)
            {
                _model = dbAccess.Where(p => p.StartBillingPeriod >= fromDate && p.StopBillingPeriod <= toDate)
                    .OrderBy(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.CustomerID == searchId || 
                                p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                p.InvoiceNumber.ToUpper().Contains(searchText.ToUpper()))
                    .OrderByDescending(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.StartBillingPeriod >= fromDate && p.StopBillingPeriod <= toDate) &&
                                (p.CustomerID == searchId || 
                                 p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.InvoiceNumber.ToUpper().Contains(searchText.ToUpper())))
                    .OrderByDescending(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.InvoiceNumber)
                    .ThenBy(p => p.CustomerID).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_CustomerMontlyTotalInvoice>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }

        // GET: CustomerMontlyTotalInvoice/Details/5
        public ActionResult Details(int? page, string billingPeriod, int? customerId)
        {
            if (billingPeriod.IsNullOrWhiteSpace())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var dbAccess = _db.view_CustomerMontlyTotalInvoice;
            
            DateTime.TryParse(billingPeriod, out var billDateTime);
            //_model = dbAccess.Where(p => p.InvoiceNumber.Contains(id.Substring(0, 4))).ToList();
            _model = dbAccess.Where(p => p.StartBillingPeriod == billDateTime).OrderBy(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();
            var inArray = _model.Select(p=>p.CustomerID).ToArray();
            if (_model == null)
            {
                return HttpNotFound();
            }

            ViewBag.BillingPeriod = billingPeriod.Trim();
            ViewBag.CustomerId = customerId;
            var index = 0;
            for (var i = 0; i < inArray.Length; i++)
            {
                if (inArray[i] == customerId)
                {
                    index = i;
                } 
            }

            if ((index + 1) < inArray.Length)
            {
                ViewBag.NextCustomerId = inArray[index + 1];
                if ((index -1) > 0 ) { ViewBag.PreviousCustomerId = inArray[index-1]; } else { ViewBag.PreviousCustomerId = inArray[index]; }
            }
            else
            {
                ViewBag.NextCustomerId = inArray[index];
                if ((index - 1) < 0) { ViewBag.PreviousCustomerId = inArray[index]; } else { ViewBag.PreviousCustomerId = inArray[index-1]; }
            }
            //_pager = new Pager(_model.Count(), page, 1);
            //_dataList = _model.Where(p=>p.CustomerID == customerId).ToList();
            //var pageList = new StaticPagedList<view_CustomerMontlyTotalInvoice>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //return View("Details", pageList);
            return View(dbAccess.FirstOrDefault(p => p.CustomerID == customerId));
        }

        public Common.ReportResult Report(string extension, int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.view_CustomerMontlyTotalInvoice;
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
                _model = dbAccess.OrderByDescending(d => d.StartBillingPeriod).ThenBy(p => p.CustomerID).ToList();
                if (_model.FirstOrDefault() != null)
                {
                    insertDateFrom = _model.FirstOrDefault().StartBillingPeriod.ToString("dd.MM.yyyy");
                    ViewBag.CurrentFrom = _model.FirstOrDefault().StartBillingPeriod.ToString("MM.yyyy");
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
            if (fromDate == toDate) toDate = toDate.AddMonths(1).AddTicks(-1);

            if (datCondition && !textCondition)
            {
                _model = dbAccess.Where(p => p.StartBillingPeriod >= fromDate && p.StopBillingPeriod <= toDate)
                    .OrderBy(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.CustomerID == searchId ||
                                p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                p.InvoiceNumber.ToUpper().Contains(searchText.ToUpper()))
                    .OrderByDescending(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.StartBillingPeriod >= fromDate && p.StopBillingPeriod <= toDate) &&
                                (p.CustomerID == searchId ||
                                 p.CustomerName.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.CustomerIdentification.ToUpper().Contains(searchText.ToUpper()) ||
                                 p.InvoiceNumber.ToUpper().Contains(searchText.ToUpper())))
                    .OrderByDescending(d => d.InvoiceNumber).ThenBy(p => p.CustomerID).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.InvoiceNumber)
                    .ThenBy(p => p.CustomerID).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();


            #region ********************* Report  *******************************
            var reportName = "FakturacneUdajeSumarne_" + DateTime.Now.ToString("MMyyyy");

            var reportFromDate = _model.Min(p => p.StartBillingPeriod).ToString("dd.MM.yyyy");
            var reportToDate = _model.Max(p => p.StopBillingPeriod).ToString("dd.MM.yyyy");
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
            report.TextFields.Title = "Sumárne fakturačné údaje";
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
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.InvoiceNumber)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.StartBillingPeriod)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.StopBillingPeriod)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.DeliveryDate)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.DueDate)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerID)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerIdentification)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerName)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.NumberOfRequest)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.ReceivedBytes)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.RequestedTime)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.MeasureOfUnits)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.TotalPriceWithoutVAT)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.VAT)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.TotalPriceWithVAT)].Hidden = true;


            return new Common.ReportResult(report) { FileName = reportName };
#endregion
        }


        public Common.ReportResult DetailReport(string extension, int? page, string billingPeriod, int? customerId)
        {
            var dbAccess = _db.view_CustomerMontlyTotalInvoice;
            DateTime.TryParse(billingPeriod, out var billDateTime);
            _model = dbAccess.Where(p => p.CustomerID == customerId).ToList();
            
            #region ******************************** Report ************************************

            var reportName = _model.FirstOrDefault().CustomerIdentification+"_FakturacneUdaje_" + DateTime.Now.ToString("MMyyyy");

            
            var reportFromDate = _model.Min(p => p.StartBillingPeriod).ToString("dd.MM.yyyy");
            var reportToDate = _model.Max(p => p.StopBillingPeriod).ToString("dd.MM.yyyy");
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
            report.TextFields.Title = "Fakturačné údaje zákazníka: " + _model.FirstOrDefault().CustomerName+ " ID Zákazníka: "+ _model.FirstOrDefault().CustomerIdentification;
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
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.InvoiceNumber)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.StartBillingPeriod)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.StopBillingPeriod)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.DeliveryDate)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.DueDate)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerID)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerIdentification)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.CustomerName)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.NumberOfRequest)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.ReceivedBytes)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.RequestedTime)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.MeasureOfUnits)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.TotalPriceWithoutVAT)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.VAT)].Hidden = true;
            //report.DataFields[nameof(view_CustomerMontlyTotalInvoice.TotalPriceWithVAT)].Hidden = true;


            return new Common.ReportResult(report) { FileName = reportName };
            #endregion
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
