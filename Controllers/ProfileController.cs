using Microsoft.AspNetCore.Mvc;

namespace shopping.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Login()]
        public ActionResult Index()
        {
            SessionService.SetProgramInfo("", "使用者");
            ActionService.SetActionName("我的帳號");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }
    }
}