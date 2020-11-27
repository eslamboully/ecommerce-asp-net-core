using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceAspCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}