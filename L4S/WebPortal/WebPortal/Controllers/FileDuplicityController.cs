using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Models;
using PagedList;
using Microsoft.Ajax.Utilities;
using WebPortal.Common;

namespace WebPortal.Controllers
{
    public class FileDuplicityController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<STInputFileDuplicity> _dataList;
        private List<STInputFileDuplicity> _model;
        private Pager _pager;

        // GET: FileDuplicity
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.STInputFileDuplicity;
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
                _model = dbAccess.Where(p => p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate)
                    .OrderBy(d => d.InsertDateTime).ToList();

            }
            if (textCondition && !datCondition)
            {
                _model = dbAccess
                    .Where(p => p.LoaderBatchID == searchId ||
                                p.FileName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.OriFileName.ToUpper().Contains(searchText.ToUpper()))
                    .OrderByDescending(d => d.InsertDateTime).ToList();

            }
            if (textCondition && datCondition)
            {
                _model = dbAccess
                    .Where(p => (p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate) &&
                                (p.LoaderBatchID == searchId ||
                                p.FileName.ToUpper().Contains(searchText.ToUpper()) ||
                                p.OriFileName.ToUpper().Contains(searchText.ToUpper())))
                    .OrderByDescending(d => d.InsertDateTime).ToList();

            }

            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.InsertDateTime).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<STInputFileDuplicity>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("Index", pageList);
        }

        // GET: FileDuplicity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            STInputFileDuplicity sTInputFileDuplicity = _db.STInputFileDuplicity.Find(id);
            if (sTInputFileDuplicity == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(sTInputFileDuplicity.ID, sTInputFileDuplicity.OriFileName);
            return PartialView("_deleteModal", model);

          }

        // POST: FileDuplicity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STInputFileDuplicity sTInputFileDuplicity = _db.STInputFileDuplicity.Find(id);
            if (sTInputFileDuplicity != null) {sTInputFileDuplicity.TCActive = 99;
            _db.SaveChanges();
            }
            return RedirectToAction("Index");
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
