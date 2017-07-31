﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;
using WebPortal.DataContexts;
using PagedList;
using WebPortal.Models;

namespace WebPortal.Controllers
{
   
    public class ProcessStatusController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: ProcessStatus
        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.ToList();
            return View(cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
        }

        // GET: ProcessStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATProcessStatus cATProcessStatus = db.CATProcessStatus.Find(id);
            if (cATProcessStatus == null)
            {
                return HttpNotFound();
            }
            return View(cATProcessStatus);
        }

        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            DateTime fromDate;
            DateTime toDate;
            DateTime.TryParse(insertDateFrom, out fromDate);
            if (!DateTime.TryParse(insertDateTo, out toDate))
            {
                toDate=DateTime.Now;
            }
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.Where(p => p.TCInsertTime >= fromDate && p.TCInsertTime <= toDate).ToList();
            if (cATProcessStatus.Count == 0)
                {
                    cATProcessStatus = db.CATProcessStatus.ToList();
                }
              return View("Index", cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
        
        }

        // GET: ProcessStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StepName,BatchID,BatchRecordNum,NumberOfService,NumberOfCustomer,NumberOfUnknownService,NumberOfPreprocessDelete,TCInsertTime")] CATProcessStatus cATProcessStatus)
        {
            if (ModelState.IsValid)
            {
                db.CATProcessStatus.Add(cATProcessStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATProcessStatus);
        }

        // GET: ProcessStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATProcessStatus cATProcessStatus = db.CATProcessStatus.Find(id);
            if (cATProcessStatus == null)
            {
                return HttpNotFound();
            }
            return View(cATProcessStatus);
        }

        // POST: ProcessStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StepName,BatchID,BatchRecordNum,NumberOfService,NumberOfCustomer,NumberOfUnknownService,NumberOfPreprocessDelete,TCInsertTime")] CATProcessStatus cATProcessStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATProcessStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATProcessStatus);
        }

        // GET: ProcessStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATProcessStatus cATProcessStatus = db.CATProcessStatus.Find(id);
            if (cATProcessStatus == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cATProcessStatus.Id, Resources.Labels.ProcessTab_Status);
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CATProcessStatus cATProcessStatus = db.CATProcessStatus.Find(id);
            //if (cATProcessStatus == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cATProcessStatus);
        }

        // POST: ProcessStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CATProcessStatus cATProcessStatus = db.CATProcessStatus.Find(id);
           // db.CATProcessStatus.Remove(cATProcessStatus);
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
