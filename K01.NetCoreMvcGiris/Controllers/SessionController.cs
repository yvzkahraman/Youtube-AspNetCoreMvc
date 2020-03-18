using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            SetSession();
            ViewBag.Kisi= GetSession();

            HttpContext.Session.SetObject("kategori", new Kategori() { Ad = "Yeni Kategori" });


            return View(HttpContext.Session.GetObject<Kategori>("kategori"));
        }

        public void SetSession()
        {
            HttpContext.Session.SetString("kisi", "yavuz");

          
            //HttpContext.Session.
        }

        public string GetSession()
        {
            return HttpContext.Session.GetString("kisi");
        }
    }
}