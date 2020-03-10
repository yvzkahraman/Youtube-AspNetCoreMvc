using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    //[Route("kullanici/[action]")]
    public class UserController : Controller
    {
        

        public IActionResult Index(string dil)
        {
            return View();
        }

        [Route("kullanici/{kisiBilgi?}")]
        public string KisiBilgi(string kisiBilgi)
        {
            return kisiBilgi;
        }
    }
}