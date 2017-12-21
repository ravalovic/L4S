using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //show errors
            if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
            //show ok message
            if (TempData["ModelStateOk"] != null) ViewBag.message = TempData["ModelStateOk"];

            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}