using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K01.NetCoreMvcGiris.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class TestController : Controller
    {
        readonly IScoped _scoped;
        readonly IScoped _scoped2;
        readonly ITransient _transient;
        readonly ITransient _transient2;
        readonly ISingleton _singleton;
        readonly ISingleton _singleton2;

        public TestController(
            IScoped scoped,
            IScoped scoped2, 
            ITransient transient, 
            ITransient transient2,
            ISingleton singleton,
            ISingleton singleton2)
        {
            _singleton = singleton;
            _singleton2 = singleton2;
            _transient = transient;
            _transient2 = transient2;
            _scoped = scoped;
            _scoped2 = scoped2;
        }

        public IActionResult Index()
        {
            ViewBag.Singleton1 = _singleton.Sayi;
            ViewBag.Singleton2 = _singleton2.Sayi;

            ViewBag.Scoped1 = _scoped.Sayi;
            ViewBag.Scoped2 = _scoped2.Sayi;

            ViewBag.Transient1 = _transient.Sayi;
            ViewBag.Transient2 = _transient2.Sayi;

            return View();
        }
    }
}