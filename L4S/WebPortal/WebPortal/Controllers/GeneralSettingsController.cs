using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.Common;
using WebPortal.DataContexts;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class GeneralSettingsController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CONFGeneralSettings
        public ActionResult Index()
        {
            return View(db.CONFGeneralSettings.Where(p => p.TCActive != 99).ToList());
        }

        // GET: CONFGeneralSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CONFGeneralSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParamName,ParamValue,Note,TCInsertTime,TCLastUpdate,TCActive")] CONFGeneralSettings cOnfGeneralSettings)
        {
            if (ModelState.IsValid)
            {
                cOnfGeneralSettings.TCActive = 0;
                cOnfGeneralSettings.TCInsertTime = DateTime.Now;
                cOnfGeneralSettings.TCLastUpdate = DateTime.Now;
                db.CONFGeneralSettings.Add(cOnfGeneralSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOnfGeneralSettings);
        }

        // GET: CONFGeneralSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFGeneralSettings cOnfGeneralSettings = db.CONFGeneralSettings.Find(id);
            if (cOnfGeneralSettings == null)
            {
                return HttpNotFound();
            }
            return View(cOnfGeneralSettings);
        }

        // POST: CONFGeneralSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParamName,ParamValue,Note,TCInsertTime,TCLastUpdate,TCActive")] CONFGeneralSettings cOnfGeneralSettings)
        {
            if (ModelState.IsValid)
            {
                cOnfGeneralSettings.TCActive = 0;
                cOnfGeneralSettings.TCLastUpdate = DateTime.Now;
                db.Entry(cOnfGeneralSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOnfGeneralSettings);
        }

        // GET: CONFGeneralSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CONFGeneralSettings cOnfGeneralSettings = db.CONFGeneralSettings.Find(id);
            if (cOnfGeneralSettings == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cOnfGeneralSettings.ID, cOnfGeneralSettings.ParamName);  
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CONFGeneralSettings cONFGeneralSettings = db.CONFGeneralSettings.Find(id);
            //if (cONFGeneralSettings == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cONFGeneralSettings);
        }

        // POST: CONFGeneralSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONFGeneralSettings cOnfGeneralSettings = db.CONFGeneralSettings.Find(id);            
            if (cOnfGeneralSettings != null)
            {
                cOnfGeneralSettings.TCActive = 99;
                cOnfGeneralSettings.TCLastUpdate = DateTime.Now;
            }
            db.SaveChanges();
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
