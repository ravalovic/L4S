using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using WebPortal.Common;
using Microsoft.Ajax.Utilities;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class UnknownServicesController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<UnknownServicesViewModel> _dataList;
        private List<CATUnknownService> _model;
        private Pager _pager;
        // GET: UnknownServices
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var modelViews = new List<UnknownServicesViewModel>();
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



            foreach (var service in _model)
            {
                modelViews.Add(new UnknownServicesViewModel
                {

                    ID = service.ID,
                    BatchID = service.BatchID,
                    RecordID = service.RecordID,
                    CustomerID = service.CustomerID,
                    //CustomerName = _db.CATCustomerData.Where(a => a.PKCustomerDataID == service.CustomerID && a.CompanyName != null).Select(a => a.CompanyName) != null ? _db.CATCustomerData.Where(c => (c.PKCustomerDataID == service.CustomerID && c.CompanyName != null)).Select(c => c.CompanyName).ToString() : _db.CATCustomerData.Where(c => (c.PKCustomerDataID == service.CustomerID && c.CompanyName == null)).Select(c => c.IndividualFirstName + " " + c.IndividualLastName).ToString(),
                    CustomerName = _db.CATCustomerData.FirstOrDefault(a => a.PKCustomerDataID == service.CustomerID && a.CompanyName != null)?.CompanyName ?? 
                    _db.CATCustomerData.FirstOrDefault(a => a.PKCustomerDataID == service.CustomerID && a.CompanyName == null)?.IndividualFirstName + " " +
                    _db.CATCustomerData.FirstOrDefault(a => a.PKCustomerDataID == service.CustomerID && a.CompanyName == null)?.IndividualLastName,
                    ServiceID = service.ServiceID,
                    DateOfRequest = service.DateOfRequest,
                    RequestedURL = service.RequestedURL,
                    RequestStatus = service.RequestStatus,
                    BytesSent = service.BytesSent,
                    UserIPAddress = service.UserIPAddress
                });
            }
            
            _pager = new Pager(modelViews.Count(), page);
            _dataList = modelViews.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<UnknownServicesViewModel>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
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
        private List<CATUnknownService> ApplyFilter(string search, int searchId, DateTime fromDate, DateTime toDate, bool txtCon, bool datCon, out bool filter)
        {
            filter = true;
            var dbAccess = _db.CATUnknownService;
            List<CATUnknownService> model = new List<CATUnknownService>();

            if (datCon && !txtCon)
            {
                model = dbAccess.Where(p => p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate)
                    .OrderBy(d => d.TCInsertTime).ToList();

            }
            if (txtCon && !datCon)
            {
                model = dbAccess
                    .Where(p => p.BatchID == searchId ||
                                p.RequestedURL.ToUpper().Contains(search.ToUpper()) ||
                                p.UserIPAddress.ToUpper().Contains(search.ToUpper()) ||
                                p.UserAgent.ToUpper().Contains(search.ToUpper()))
                    .OrderByDescending(d => d.DateOfRequest).ToList();

            }
            if (datCon && txtCon)
            {
                model = dbAccess
                    .Where(p => (p.DateOfRequest.Value >= fromDate && p.DateOfRequest.Value <= toDate) &&
                                (p.BatchID == searchId ||
                                 p.RequestedURL.ToUpper().Contains(search.ToUpper()) ||
                                 p.UserIPAddress.ToUpper().Contains(search.ToUpper()) ||
                                 p.UserAgent.ToUpper().Contains(search.ToUpper())))
                    .OrderByDescending(d => d.DateOfRequest).ToList();

            }

            if (model.Count == 0)
            {
                model = dbAccess.OrderByDescending(d => d.TCInsertTime).ToList();
                filter = false;
            }
            return model;
        }
    }
}
