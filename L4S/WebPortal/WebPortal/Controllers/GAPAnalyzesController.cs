using System.Linq;
using System.Web.Mvc;
using WebPortal.Common;
using WebPortal.DataContexts;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
    public class GapAnalyzesController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: GAPAnalyzes
        public ActionResult Index()
        {
            return View(db.GAPAnalyze.OrderByDescending(d => d.Id).ToList());
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
