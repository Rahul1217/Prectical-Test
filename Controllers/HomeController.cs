using System.Web.Mvc;

namespace Practical_Test_Interview.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Examples()
        {
            return View();
        }
    }
}
