using Microsoft.AspNetCore.Mvc;

namespace shopping.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

    }
}
