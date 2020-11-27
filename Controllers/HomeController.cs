using System;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceAspCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}