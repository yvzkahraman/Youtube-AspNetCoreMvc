using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class CookieController : Controller
    {

        public IActionResult Index()
        {
            CookieOlustur();
            ViewBag.GelenDeger = CookieGetir();
            return View();
        }
        public void CookieOlustur()
        {
            HttpContext.Response.Cookies.Append("CookieYavuz", "Yavuz");
        }
        public string CookieGetir()
        {
            return Request.Cookies["CookieYavuz"]?.ToString();
        }
        
    }
}