using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyTemp()
        {
            return View();
        }
        [Authorize(Roles = "BloodBank")]
        public IActionResult test()
        {
            return View();
        }
    }
}
