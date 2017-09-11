using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.Common;
using WebPortal.Models;
using WebPortal.DataContexts;

namespace WebPortal.Controllers
{
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class OwnerDatasController : Controller
    {
        private L4SDb _db = new L4SDb();

        public ActionResult SetActive(int id)
        {
            var actualList = _db.CATOwnerData.Where(o => o.TCActive != 99).ToList();
            foreach (var item in actualList)
            {
                if (item.ID == id) item.TCActive = 1;
                else item.TCActive = 0;
            }
            if (actualList.Count == 1)
            {
                var firstOrDefault = actualList.FirstOrDefault();
                if (firstOrDefault != null) firstOrDefault.TCActive = 1;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: CATOwnerDatas/Create
        public ActionResult Create()
        {   
            return View();
        }

        public ActionResult Index()
        {
            return View(_db.CATOwnerData.Where(o => o.TCActive != 99).ToList());
        }
        // POST: CATOwnerDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            //[Bind(Include = "ID,OwnerCompanyName,OwnwerCompanyType,OwnerCompanyType,OwnerCompanyID,OwnerCompanyTAXID,OwnerCompanyVATID,OwnerBankAccountIban,OwnerAddressStreet,OwnerAddressBuildingNumber,OwnerAddressCity,OwnerAddressZipCode,OwnerAddressCountry,OwnerResponsibleFirstName,OwnerResponsiblelastName,OwnerContactEmail,OwnerContactMobile,OwnerContactPhone,OwnerContactWeb,TCInsertTime,TCLastUpdate,TCActive")]
        CATOwnerData cATOwnerData)
        {
            if (ModelState.IsValid)
            {
                cATOwnerData.TCLastUpdate = DateTime.Now;
                cATOwnerData.TCActive = 0;
                cATOwnerData.TCInsertTime = DateTime.Now;
                _db.CATOwnerData.Add(cATOwnerData);
                _db.SaveChanges();
                return RedirectToAction("SetActive", new{cATOwnerData.ID});
            }

            return View(cATOwnerData);
        }

        // GET: CATOwnerDatas/Edit/5
        public ActionResult Edit()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            CATOwnerData cATOwnerData = _db.CATOwnerData.FirstOrDefault();
            if (cATOwnerData == null)
            {
                return RedirectToAction("Create");
            }
            return View(cATOwnerData);
        }

        // POST: CATOwnerDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OwnerCompanyName,OwnwerCompanyType,OwnerCompanyType,OwnerCompanyID,OwnerCompanyTAXID,OwnerCompanyVATID,OwnerBankAccountIban,OwnerAddressStreet,OwnerAddressBuildingNumber,OwnerAddressCity,OwnerAddressZipCode,OwnerAddressCountry,OwnerResponsibleFirstName,OwnerResponsiblelastName,OwnerContactEmail,OwnerContactMobile,OwnerContactPhone,OwnerContactWeb,TCInsertTime,TCLastUpdate,TCActive")] CATOwnerData cATOwnerData)
        {
            if (ModelState.IsValid)
            {
                cATOwnerData.TCLastUpdate = DateTime.Now;
                cATOwnerData.TCActive = 0;
                cATOwnerData.TCInsertTime = DateTime.Now;
                _db.Entry(cATOwnerData).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("SetActive", new { cATOwnerData.ID });
            }
            return RedirectToAction("Index"); 
        }
        // GET: FileInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATOwnerData catOwnerData = _db.CATOwnerData.Find(id);
            if (catOwnerData == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(catOwnerData.ID, catOwnerData.OwnerCompanyName);
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
            CATOwnerData catOwnerData = _db.CATOwnerData.Find(id);
            catOwnerData.TCActive = 99;
            _db.SaveChanges();
            return RedirectToAction("SetActive", new{catOwnerData.ID});
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
