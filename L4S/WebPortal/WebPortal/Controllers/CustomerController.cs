using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Models;
using WebPortal.Common;

namespace WebPortal.Controllers
{
    public class CustomerController : Controller
    {
        private L4SDb _db = new L4SDb();
        private Pager _pager;
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
            CATCustomerData cAtCustomerData = _db.CATCustomerData.Find(id);
            if (cAtCustomerData == null)
            {
                return HttpNotFound();
            }
            if (cAtCustomerData.CustomerType == "PO") return PartialView("_CompanyDetails", cAtCustomerData);
            return PartialView("_IndividualDetails", cAtCustomerData);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? page, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData model = _db.CATCustomerData.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).Include(p => p.CATCustomerServices).FirstOrDefault();
            var dataList = _db.CATCustomerData.Where(l => l.PKCustomerDataID == id).ToList();
            if (model == null)
            {
                return HttpNotFound();
            }

            //if (model.CustomerType == "PO") ViewBag.CustomerType = 1;
            //else ViewBag.CustomerType = 2;

            _pager = new Pager(1, page);
            var pageList = new StaticPagedList<CATCustomerData>(dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("CompanyList", pageList);

            //return View("Details", cATCustomerData);          
        }

        // GET: Customer/Services/5
        public ActionResult Services(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerData customer = _db.CATCustomerData.Find(id); //.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }

            //get list of all services checked, unchecked for customer
            CustomerViewModel model = new CustomerViewModel(customer); 
            //if (customer.CustomerType == "PO") ViewBag.CustomerType = 1;
            //else ViewBag.CustomerType = 2;

            return View("Details", model);
        }

        // GET: Customer/Services/5
        public ActionResult Identifiers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerData cAtCustomerData = _db.CATCustomerData.Where(l => l.PKCustomerDataID == id).Include(p => p.CATCustomerIdentifiers).FirstOrDefault();

            if (cAtCustomerData == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel model = new CustomerViewModel(cAtCustomerData);
            //if (cAtCustomerData.CustomerType == "PO") ViewBag.CustomerType = 1;
            //else ViewBag.CustomerType = 2;

            model.Services = null; //show only identifier
            return View("Details", model);
        }

        // POST: Customer/EditServices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveServices(List<ServicesViewModel> data)
        {
           // if (ModelState.IsValid)
            if (ModelState.IsValid && data.Count > 0)
            {
                CATCustomerData customer = _db.CATCustomerData.Find(data[0].FKCustomerDataID);

                foreach (ServicesViewModel item in data)
                {

                    if (item.Checked && item.TCActive == 1) //edit customer already assigned service
                    {
                        CATCustomerServices service = customer.CATCustomerServices.Where(p => p.PKServiceCustomerIdentifiersID == item.PKServiceCustomerIdentifiersID).FirstOrDefault();
                        service.ServiceCode = item.ServiceCode;
                        service.ServiceName = item.ServiceName;
                        service.ServiceNote = item.ServiceNote;
                        service.ServicePriceDiscount = item.ServicePriceDiscount;
                        service.TCLastUpdate = DateTime.Now;
                    }
                    else if (!item.Checked && item.TCActive == 1) //remove customer already assigned service
                    {
                        CATCustomerServices service = customer.CATCustomerServices.Where(p => p.PKServiceCustomerIdentifiersID == item.PKServiceCustomerIdentifiersID).FirstOrDefault();
                        service.TCLastUpdate = DateTime.Now;
                        service.TCActive = 99;
                    }
                    else if (item.Checked && item.TCActive == 0) //add new service to customer
                    {
                        CATCustomerServices service = new CATCustomerServices();
                        service.ServiceCode = item.ServiceCode;
                        service.ServiceName = item.ServiceName;
                        service.ServiceNote = item.ServiceNote;
                        service.ServicePriceDiscount = item.ServicePriceDiscount;
                        service.FKCustomerDataID = item.FKCustomerDataID;
                        service.FKServiceID = item.FKServiceID;
                        service.TCActive = 1;
                        service.TCInsertTime = DateTime.Now;
                        service.TCLastUpdate = DateTime.Now;

                        customer.CATCustomerServices.Add(service); //add to customer new service
                    }
                }

                _db.SaveChanges();


                if (customer.CustomerType == "PO") return RedirectToAction("CompanyList");
                return RedirectToAction("IndividualList");
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));

                //Log This exception to ELMAH:
                Exception exception = new Exception(message.ToString());
               

                //Return Status Code:
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }
            return RedirectToAction("CompanyList");
        }


        // GET: Customer/IndividualList/
        public ActionResult Search(int? page, string customerType, string searchText, string currentFilter)
        {
            StaticPagedList<CATCustomerData> pageList;
            if (searchText.IsNullOrWhiteSpace())
            {
                searchText = currentFilter;
            }

            // set actual filter to ViewBag
            ViewBag.CurrentFilter = searchText;

            List<CATCustomerData> model = _db.CATCustomerData.ToList();
            List<CATCustomerData> dataList;


            if (customerType.Equals("PO"))
            {
                dataList = model.Where(p => p.CustomerType.ToUpper().Equals(customerType.ToUpper())
                                            && (p.CompanyName.ToUpper().Contains(searchText.ToUpper())
                                                || p.CompanyID.ToUpper().Contains(searchText.ToUpper())
                                                || p.Address.ToUpper().Contains(searchText.ToUpper()))).ToList();
            }
            else
            {
                dataList = model.Where(p => p.CustomerType.ToUpper().Equals(customerType.ToUpper())
                                            && (p.IndividualFirstName.ToUpper().Contains(searchText.ToUpper())
                                            || p.IndividualLastName.ToUpper().Contains(searchText.ToUpper())
                                            || p.IndividualID.ToUpper().Contains(searchText.ToUpper())
                                                || p.Address.ToUpper().Contains(searchText.ToUpper()))).ToList();
            }

            if (dataList.Count > 0)
            {
                _pager = new Pager(dataList.Count, page);
                pageList = new StaticPagedList<CATCustomerData>(dataList, _pager.CurrentPage, _pager.PageSize,
                    _pager.TotalItems);
                if (customerType.Contains("FO"))
                {
                    return View("IndividualList", pageList);
                }
                return View("CompanyList", pageList);
            }
            
            dataList = model.Where(p => p.CustomerType.Equals(customerType)).ToList();
            _pager = new Pager(model.Count, page);
            pageList = new StaticPagedList<CATCustomerData>(dataList, _pager.CurrentPage, _pager.PageSize,
                _pager.TotalItems);
            if (customerType.Contains("FO"))
            {
                return View("IndividualList", pageList);
            }
            return View("CompanyList", pageList);

        }

        // GET: Customer/IndividualList/
        public ActionResult IndividualList(int? page)
        {
            List<CATCustomerData> model = _db.CATCustomerData.Where(p => p.CustomerType == "FO" && p.TCActive != 99).ToList();
            _pager = new Pager(model.Count(), page);
            var pageList = new StaticPagedList<CATCustomerData>(model, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("IndividualList", pageList);
            //return View(model);
        }

        // GET: Customer/ComapnyList/
        public ActionResult CompanyList(int? page)
        {
            List<CATCustomerData> model = _db.CATCustomerData.Where(p => p.CustomerType == "PO" && p.TCActive != 99).ToList();
            //return View(model);
            _pager = new Pager(model.Count(), page);
            var pageList = new StaticPagedList<CATCustomerData>(model, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            return View("CompanyList", pageList);
        }

        // GET: Customer/Create/
        public ActionResult Create(int? id)
        {
            CATCustomerData cAtCustomerData = new CATCustomerData();

            if (id == 1)
            {
                cAtCustomerData.CustomerType = "PO";
                return PartialView("_CompanyCreate", cAtCustomerData);
            }
            else if (id == 2)
            {
                cAtCustomerData.CustomerType = "FO";
                return PartialView("_IndividualCreate", cAtCustomerData);
            }
            else return RedirectToAction("CompanyList"); //return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATCustomerData cAtCustomerData)
        {
            if (ModelState.IsValid)
            {
                cAtCustomerData.TCActive = 0;
                cAtCustomerData.TCInsertTime = DateTime.Now;
                cAtCustomerData.TCLastUpdate = DateTime.Now;
                _db.CATCustomerData.Add(cAtCustomerData);
                _db.SaveChanges();
            }

            if (cAtCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerData cAtCustomerData = _db.CATCustomerData.Find(id);
            if (cAtCustomerData == null)
            {
                return HttpNotFound();
            }
            if (cAtCustomerData.CustomerType == "PO") return PartialView("_CompanyEdit", cAtCustomerData);
            return PartialView("_IndividualEdit", cAtCustomerData);
        }

        //[Bind(Include = "PKCustomerDataID,CustomerType,CompanyName,CompanyType,CompanyID,CompanyTAXID,CompanyVATID,IndividualTitle,IndividualFirstName,IndividualLastName,IndividualID,IndividualTAXID,IndividualVATID,BankAccountIBAN,AddressStreet,AddressBuildingNumber,AddressCity,AddressZipCode,AddressCountry,ContactEmail,ContactMobile,ContactPhone,ContactWeb,TCInsertTime,TCLastUpdate,TCActive")] 
        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CATCustomerData cAtCustomerData)
        {
            if (ModelState.IsValid)
            {
                cAtCustomerData.TCLastUpdate = DateTime.Now;
                _db.Entry(cAtCustomerData).State = EntityState.Modified;
                _db.SaveChanges();
                // return RedirectToAction("CompanyList");
            }
            if (cAtCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            return RedirectToAction("IndividualList");
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerData cAtCustomerData = _db.CATCustomerData.Find(id);
            if (cAtCustomerData == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cAtCustomerData.PKCustomerDataID,
                cAtCustomerData.IndividualFirstName + ' ' + cAtCustomerData.IndividualLastName);
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
            CATCustomerData cAtCustomerData = _db.CATCustomerData.Find(id);
            if (cAtCustomerData != null)
            {
                cAtCustomerData.TCActive = 99;
                _db.SaveChanges();

                if (cAtCustomerData.CustomerType == "PO") return RedirectToAction("CompanyList");
            }
            return RedirectToAction("IndividualList");
        }

        // GET: Customer/AddIdentifier/5
        public ActionResult AddIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cAtCustomerIdent = new CATCustomerIdentifiers { FKCustomerID = id.Value };


            return PartialView("_AddIdentifier", cAtCustomerIdent);
        }


        ////  POST- CREATE new ADD Identifier ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIdentifier(CATCustomerIdentifiers cAtCustomerIdent)
        {
            if (ModelState.IsValid)
            {
                cAtCustomerIdent.TCLastUpdate = DateTime.Now;
                cAtCustomerIdent.TCInsertTime = DateTime.Now;
                cAtCustomerIdent.TCActive = 0;

                _db.CATCustomerIdentifiers.Add(cAtCustomerIdent);
                _db.SaveChanges();
            }
            return RedirectToAction("Services", new { id = cAtCustomerIdent.FKCustomerID });
        }



        // GET: Customer/EditIdentifier/5
        public ActionResult EditIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cAtCustomerIdent = _db.CATCustomerIdentifiers.Find(id);
            if (cAtCustomerIdent == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditIdentifier", cAtCustomerIdent);
        }


        //// POST EDIT Identifier - modal  ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIdentifier(CATCustomerIdentifiers cAtCustomerIdent)
        {
            if (ModelState.IsValid)
            {
                cAtCustomerIdent.TCLastUpdate = DateTime.Now;
                _db.Entry(cAtCustomerIdent).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return RedirectToAction("Services", new { id = cAtCustomerIdent.FKCustomerID });
        }

        // GET DELETE Identifier :  Customer/DeleteIdentifier/5
        public ActionResult DeleteIdentifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATCustomerIdentifiers cAtCustomerIdent = _db.CATCustomerIdentifiers.Find(id);
            if (cAtCustomerIdent == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cAtCustomerIdent.PKCustomerIdentifiersID, "Identifikátor");
            return PartialView("_deleteModal", model);
        }


        ////    POST  DELETE Identifier ////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIdentifier(DeleteModel model)
        {
            if (ModelState.IsValid)
            {
                CATCustomerIdentifiers cAtCustomerIdent = _db.CATCustomerIdentifiers.Find(model.Id);
                if (cAtCustomerIdent != null)
                {
                    cAtCustomerIdent.TCLastUpdate = DateTime.Now;
                    cAtCustomerIdent.TCActive = 99;
                    _db.Entry(cAtCustomerIdent).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Services", new { id = cAtCustomerIdent.FKCustomerID });
                }
            }

            return HttpNotFound();
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
