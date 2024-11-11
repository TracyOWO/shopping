using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using shopping.Models;

namespace shopping.Controllers;

[Area("Admin")]
public class HomeController : Controller
{

    [HttpGet]
    [Login(RoleList = "User,Mis,Member")]
    public IActionResult Init()
    {
        SessionService.SetPrgInit();
        return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
    }


    [HttpGet]
    [Login(RoleList = "User,Mis,Member")]
    public IActionResult Index()
    {
        SessionService.SetProgramInfo("", "儀表板", false, false, 0);
        SessionService.SetActionInfo(enAction.Dashboard, enCardSize.Max);
        return View();
    }
}
