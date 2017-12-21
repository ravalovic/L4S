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
    public class ServiceController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CATServiceParameters
        public ActionResult Index()
        {
            return View(db.CATServiceParameters.Where(p=>p.TCActive!=99).ToList());
        }

        // GET: CATServiceParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServiceParameters cAtServiceParameters = db.CATServiceParameters.Find(id);
            if (cAtServiceParameters == null)
            {
                return HttpNotFound();
            }
            return View(cAtServiceParameters);
        }

        // GET: CATServiceParameters/Create
        public ActionResult Create()
        {
            CATServiceParameters model = new CATServiceParameters();
            return PartialView("_Create", model);
        }

        // POST: CATServiceParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKServiceID,ServiceCode,ServiceDescription,ServiceBasicPrice,TCInsertTime,TCLastUpdate,TCActive")] CATServiceParameters cAtServiceParameters)
        {
            if (ModelState.IsValid)
            {
                db.CATServiceParameters.Add(cAtServiceParameters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAtServiceParameters);
        }

        // GET: CATServiceParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServiceParameters cAtServiceParameters = db.CATServiceParameters.Find(id);
            if (cAtServiceParameters == null)
            {
                return HttpNotFound();
            }
            //return View(cATServiceParameters);
            return PartialView("_Edit", cAtServiceParameters);
        }

        // POST: CATServiceParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKServiceID,ServiceCode,ServiceDescription,ServiceBasicPrice,TCInsertTime,TCLastUpdate,TCActive")] CATServiceParameters cAtServiceParameters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAtServiceParameters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAtServiceParameters);
        }

        // GET: CATServiceParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATServiceParameters cAtServiceParameters = db.CATServiceParameters.Find(id);
            if (cAtServiceParameters == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cAtServiceParameters.PKServiceID, cAtServiceParameters.ServiceDescription);
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CATServiceParameters cATServiceParameters = db.CATServiceParameters.Find(id);
            //if (cATServiceParameters == null)
            //{
            //    return HttpNotFound();
            //}
            //// return View(cATServiceParameters);
            //return PartialView("_Delete", cATServiceParameters);
        }

        // POST: CATServiceParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATServiceParameters cAtServiceParameters = db.CATServiceParameters.Find(id);         
            cAtServiceParameters.TCActive = 99;
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
