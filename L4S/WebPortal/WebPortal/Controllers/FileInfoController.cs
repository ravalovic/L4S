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
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class FileInfoController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<STInputFileInfo> _dataList;
        private List<STInputFileInfo> _model;
        private Pager _pager;
        // GET: FileInfo
        
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
            var pageList = new StaticPagedList<STInputFileInfo>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }


        // GET: FileInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            STInputFileInfo sTInputFileInfo = _db.STInputFileInfo.Find(id);
            if (sTInputFileInfo == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(sTInputFileInfo.Id, sTInputFileInfo.OriFileName);
            return PartialView("_deleteModal", model);

         }

        // POST: FileInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STInputFileInfo sTInputFileInfo = _db.STInputFileInfo.Find(id);
            if (sTInputFileInfo != null) {sTInputFileInfo.TCActive = 99;
            _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ReportResult Report(string extension, int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
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

            // Create the report and turn our query into a ReportSource
            var reportName = "InputFileReport_" + DateTime.Now.ToString("ddMMyyyy");
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
                    if (confGeneralSettings.ParamValue.Equals("Landscape")) { 
                    report.RenderHints.Orientation = ReportOrientation.Landscape;
                    }
                }
            }

            //Header report
            report.TextFields.Title = "Zoznam spracovaných súborov";
            report.TextFields.SubTitle = "Obdobie od: " + insertDateFrom +" do: "+insertDateTo;
            //report.TextFields.Footer = "Copyright 2017 (c) BlueZ, s.r.o.";
            report.TextFields.Header = string.Format(@"
                Report vytvorený: {0}
                Počet súborov: {1}
                ", DateTime.Now, _model.Count);

            // Render hints allow you to pass additional hints to the reports as they are being rendered
            report.RenderHints.BooleanCheckboxes = true;
            report.RenderHints.BooleansAsYesNo = true;

            //Data fields
            report.DataFields[nameof(STInputFileInfo.Id)].Hidden = true; 
            //report.DataFields["FileName"];
            report.DataFields["Checksum"].Hidden = true; 
            //report.DataFields["LinesInFile"];
            //report.DataFields["InsertDateTime"];
            //report.DataFields["LoaderBatchID"];
            //report.DataFields["LoadedRecord"];
            //report.DataFields["OriFileName"];
            report.DataFields["OriginalFileChecksum"].Hidden = true; 
            report.DataFields["TCLastUpdate"].Hidden = true; 
            report.DataFields["TCActive"].Hidden = true; 

            // Return the ReportResult
            // the type of report that is rendered will be determined by the extension in the URL (.pdf, .xls, .html, etc)

            return new ReportResult(report) { FileName = reportName };
            
        }

        private List<STInputFileInfo> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter)
        {
            filter = true;
            var dbAccess = _db.STInputFileInfo;
            List<STInputFileInfo> model = new List<STInputFileInfo>();

            if (datCon && !txtCon)
            {
                model = dbAccess.Where(p => p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate)
                    .OrderBy(d => d.InsertDateTime).ToList();
                
            }
            if (txtCon && !datCon)
            {
                model = dbAccess
                    .Where(p => p.LoaderBatchID == searchId ||
                                p.FileName.ToUpper().Contains(search.ToUpper()) ||
                                p.OriFileName.ToUpper().Contains(search.ToUpper()))
                    .OrderByDescending(d => d.InsertDateTime).ToList();
                
            }
            if (txtCon && datCon)
            {
                model = dbAccess
                    .Where(p => (p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate) &&
                                (p.LoaderBatchID == searchId ||
                                 p.FileName.ToUpper().Contains(search.ToUpper()) ||
                                 p.OriFileName.ToUpper().Contains(search.ToUpper())))
                    .OrderByDescending(d => d.InsertDateTime).ToList();
                
            }

            if (model.Count == 0)
            {
                model = dbAccess.OrderByDescending(d => d.InsertDateTime).ToList();
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
