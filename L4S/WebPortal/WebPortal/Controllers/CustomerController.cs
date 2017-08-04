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
using WebPortal.Models;

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
            CATCustomerData cATCustomerData = db.CATCustomerData.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).Include(p => p.CATCustomerServices).FirstOrDefault(); 
            
            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }

            if (cATCustomerData.CustomerType == "PO") ViewBag.CustomerType = 1;
            else ViewBag.CustomerType = 2;
            
            return View("Details", cATCustomerData);          
        }

        // GET: Customer/Services/5
        public ActionResult Services(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerData cATCustomerData = db.CATCustomerData.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).FirstOrDefault();

            if (cATCustomerData == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel model = new CustomerViewModel(cATCustomerData);
            if (cATCustomerData.CustomerType == "PO") ViewBag.CustomerType = 1;
            else ViewBag.CustomerType = 2;

            return View("Details",model);
        }


        // POST: Customer/EditServices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveServices(List<WebPortal.Models.ServicesViewModel> data)
        {
            if (ModelState.IsValid)
            {
              //  db.CATCustomerData.Add(cATCustomerData);
             //   db.SaveChanges();
            }

           // if (cATCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
        }


        // GET: Customer/IndividualList/
        public ActionResult Search(string Name)
        {

            List<CATCustomerData> model = db.CATCustomerData.Where(p => p.TCActive != 99 && (p.CompanyName.Contains(Name) || p.IndividualLastName.Contains(Name))).ToList();
            if (model.Exists(p => p.CustomerType.Equals("PO")))
            {
                model = db.CATCustomerData.Where(p => p.CustomerType.Equals("PO") && p.CompanyName.Contains(Name) && p.TCActive != 99).ToList();
                if (model.Count == 0)
                {
                    model = db.CATCustomerData.Where(p => p.CustomerType.Equals("PO")).ToList();
                }
                return View("CompanyList", model);
            }
            else
            {
                model = db.CATCustomerData.Where(p => p.CustomerType.Equals("FO") && p.IndividualLastName.Contains(Name) && p.TCActive != 99).ToList();
            
            if (model.Count == 0)
            {
                    model = db.CATCustomerData.Where(p => p.CustomerType.Equals("FO")).ToList();
            }
                return View("IndividualList", model);
            }


            // return PartialView("_IndividualList", model);
            
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
                cATCustomerData.TCActive = 0;
                cATCustomerData.TCInsertTime = DateTime.Now;
                cATCustomerData.TCLastUpdate = DateTime.Now;
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

            DeleteModel model = new DeleteModel(cATCustomerData.PKCustomerDataID,
                cATCustomerData.IndividualFirstName + ' ' + cATCustomerData.IndividualLastName);
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CATCustomerData cATCustomerData = db.CATCustomerData.Find(id);
            //if (cATCustomerData == null)
            //{
            //    return HttpNotFound();
            //}
            //return PartialView("_Delete", cATCustomerData);
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

        // GET: Customer/AddIdentifier/5
        public ActionResult AddIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cATCustomerIdent = new CATCustomerIdentifiers();
            cATCustomerIdent.FKCustomerID = id.Value;


            return PartialView("_AddIdentifier", cATCustomerIdent);
        }


        ////  POST- CREATE new ADD Identifier ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIdentifier(CATCustomerIdentifiers cATCustomerIdent)
        {
            if (ModelState.IsValid)
            {
                cATCustomerIdent.TCLastUpdate = DateTime.Now;
                cATCustomerIdent.TCInsertTime = DateTime.Now;
                cATCustomerIdent.TCActive = 0;

                db.CATCustomerIdentifiers.Add(cATCustomerIdent);
                db.SaveChanges();           
            }           
            return RedirectToAction("Services", new { id = cATCustomerIdent.FKCustomerID });
        }



        // GET: Customer/EditIdentifier/5
        public ActionResult EditIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cATCustomerIdent = db.CATCustomerIdentifiers.Find(id);
            if (cATCustomerIdent == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditIdentifier", cATCustomerIdent);
        }


        //// POST EDIT Identifier - modal  ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIdentifier(CATCustomerIdentifiers cATCustomerIdent)
        {
            if (ModelState.IsValid)
            {
                cATCustomerIdent.TCLastUpdate = DateTime.Now;
                db.Entry(cATCustomerIdent).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Services", new { id = cATCustomerIdent.FKCustomerID });
        }

        // GET DELETE Identifier :  Customer/DeleteIdentifier/5
        public ActionResult DeleteIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cATCustomerIdent = db.CATCustomerIdentifiers.Find(id);
            if (cATCustomerIdent == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cATCustomerIdent.PKCustomerIdentifiersID, "Identifikátor");
            return PartialView("_deleteModal", model);
        }


        ////    POST  DELETE Identifier ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIdentifier(DeleteModel model)
        {
            if (ModelState.IsValid)
            {
                CATCustomerIdentifiers cATCustomerIdent = db.CATCustomerIdentifiers.Find(model.Id);
                cATCustomerIdent.TCLastUpdate = DateTime.Now;
                cATCustomerIdent.TCActive = 99;
                db.Entry(cATCustomerIdent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Services", new { id = cATCustomerIdent.FKCustomerID });
            }

            return HttpNotFound();
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
