﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Models;
using PagedList;
using Microsoft.Ajax.Utilities;

namespace WebPortal.Controllers
{
    public class FileInfoController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<STInputFileInfo> _dataList;
        private List<STInputFileInfo> _model;
        private Pager _pager;
        // GET: FileInfo
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo)
        {
            var dbAccess = _db.STInputFileInfo;
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

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            //if (sTInputFileInfo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sTInputFileInfo);
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
