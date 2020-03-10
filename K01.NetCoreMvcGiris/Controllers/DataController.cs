using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {          
            //ViewData["Kisi"] = "Yavuz";
            //ViewBag.Kisi = "Ahmet";
            //TempData["Kisi"] = "Ayşe";
            return View();
        }

        public IActionResult Temp()
        {
            ViewData["Kisi"] = "Yavuz";
            ViewBag.Kisi = "Yavuz";
            TempData["Kisi"] = "Tuğba";
                     
            return RedirectToAction("Index");
        }

    }
}