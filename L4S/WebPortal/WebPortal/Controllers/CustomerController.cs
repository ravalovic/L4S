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
            return RedirectToAction("CompanyList");
        }

        // GET: Customer/Details/5
        public ActionResult CustomerDetails(int? id)
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
            if (cATCustomerData.CustomerType == "PO") return PartialView("_CompanyDetails", cATCustomerData);
            return PartialView("_IndividualDetails", cATCustomerData);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData cATCustomerData = db.CATCustomerData.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).FirstOrDefault(); //db.CATCustomerData.Find(id);
            
            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }

            ////load list data from DB
            //db.Entry(cATCustomerData).Collection(x => x.CATCustomerIdentifiers).Load();
            //zoznam = db.Miesta.Include(l => l.Karty).Where(k => k.TypMiestoId == 1).ToList();
            //cATCustomerData.CATCustomerIdentifiers = db.CATCustomerIdentifiers.Include(l => l.).Where(k => k.TypMiestoId == 1).ToList();

            if (cATCustomerData.CustomerType == "PO") ViewBag.CustomerType = 1;
            else ViewBag.CustomerType = 2;
            
            return View("Details", cATCustomerData);          
        }

        // GET: Customer/IndividualList/
        public ActionResult Search(string Name)
        {
            List<CATCustomerData> model = db.CATCustomerData.Where(p =>(p.CompanyName.Contains(Name) || p.IndividualLastName.Contains(Name)) && p.TCActive != 99).ToList();
            // return PartialView("_IndividualList", model);
            return View("CompanyList", model);
        }

        // GET: Customer/IndividualList/
        public ActionResult IndividualList()
        {
            List<CATCustomerData> model = db.CATCustomerData.Where(p => p.CustomerType == "FO" && p.TCActive != 99).ToList();
            return View(model);
        }

        // GET: Customer/ComapnyList/
        public ActionResult CompanyList()
        {
            List<CATCustomerData> model = db.CATCustomerData.Where(p => p.CustomerType == "PO" && p.TCActive!=99).ToList();
            return View(model);
        }

        // GET: Customer/Create/
        public ActionResult Create(int? id)
        {
            CATCustomerData cATCustomerData = new CATCustomerData();

            if (id == 1)
            {
                cATCustomerData.CustomerType = "PO";
                return PartialView("_CompanyCreate", cATCustomerData);
            }
            else if (id == 2)
            {
                cATCustomerData.CustomerType = "FO";
                return PartialView("_IndividualCreate", cATCustomerData);
            }
            else return RedirectToAction("CompanyList"); //return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATCustomerData cATCustomerData)
        {
            if (ModelState.IsValid)
            {
                db.CATCustomerData.Add(cATCustomerData);
                db.SaveChanges();
            }

            if (cATCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
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
            if (cATCustomerData.CustomerType == "PO") return PartialView("_CompanyEdit", cATCustomerData);
            return PartialView("_IndividualEdit", cATCustomerData);
        }

        //[Bind(Include = "PKCustomerDataID,CustomerType,CompanyName,CompanyType,CompanyID,CompanyTAXID,CompanyVATID,IndividualTitle,IndividualFirstName,IndividualLastName,IndividualID,IndividualTAXID,IndividualVATID,BankAccountIBAN,AddressStreet,AddressBuildingNumber,AddressCity,AddressZipCode,AddressCountry,ContactEmail,ContactMobile,ContactPhone,ContactWeb,TCInsertTime,TCLastUpdate,TCActive")] 
        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CATCustomerData cATCustomerData)
        {
            if (ModelState.IsValid)
            {
                cATCustomerData.TCLastUpdate = DateTime.Now;
                db.Entry(cATCustomerData).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("CompanyList");
            }
            if (cATCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
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
            return PartialView("_Delete", cATCustomerData);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            cATCustomerData.TCActive = 99;
            db.SaveChanges();

            if (cATCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
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
