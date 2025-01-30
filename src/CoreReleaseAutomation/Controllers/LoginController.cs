using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreReleaseAutomation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreReleaseAutomation.Controllers
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult Index(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            if (!Helpers.AuthenticationHelper.Login(login.UserName, login.Password))
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(login);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}