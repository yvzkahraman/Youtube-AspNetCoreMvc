using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K01.NetCoreMvcGiris.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class TagController : Controller
    {
        IKategoriRepository _kategoriRepository;
        public TagController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public IActionResult Index(int aktifSayfa=1)
        {
            ViewBag.AktifSayfa = aktifSayfa;
            return View(_kategoriRepository.HepsiniGetir());
        }
    }
}