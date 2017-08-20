using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? LoginError)
        {
            if (LoginError==true) ModelState.AddModelError("", "Nespravne meno alebo Heslo.");
            DataContexts.L4SDb _db = new DataContexts.L4SDb();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}