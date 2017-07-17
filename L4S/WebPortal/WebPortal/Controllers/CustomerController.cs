using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;
using WebPortal.DataContexts;

namespace WebPortal.Controllers
{
    public class CustomerController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: Customer
        public ActionResult Index()
        {
            return View(db.CATCustomerData.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerData);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKCustomerDataID,CustomerType,CompanyName,CompanyType,CompanyID,CompanyTAXID,CompanyVATID,IndividualTitle,IndividualFirstName,IndividualLastName,IndividualID,IndividualTAXID,IndividualVATID,BankAccountIBAN,AddressStreet,AddressBuildingNumber,AddressCity,AddressZipCode,AddressCountry,ContactEmail,ContactMobile,ContactPhone,ContactWeb,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerData cATCustomerData)
        {
            if (ModelState.IsValid)
            {
                db.CATCustomerData.Add(cATCustomerData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATCustomerData);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerData);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKCustomerDataID,CustomerType,CompanyName,CompanyType,CompanyID,CompanyTAXID,CompanyVATID,IndividualTitle,IndividualFirstName,IndividualLastName,IndividualID,IndividualTAXID,IndividualVATID,BankAccountIBAN,AddressStreet,AddressBuildingNumber,AddressCity,AddressZipCode,AddressCountry,ContactEmail,ContactMobile,ContactPhone,ContactWeb,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerData cATCustomerData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATCustomerData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATCustomerData);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerData);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            db.CATCustomerData.Remove(cATCustomerData);
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
