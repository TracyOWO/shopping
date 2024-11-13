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
        [HttpGet]
        [Login()]
        public ActionResult Edit()
        {
            SessionService.SetProgramInfo("", "我的帳號");
            ActionService.SetActionName("修改個人資料");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public ActionResult Edit(Users model)
        {
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();
            user.UpdateUserProfile(model);
            return RedirectToAction("Index", "Profile", new { area = "" });
        }
    }
}