
using K01.NetCoreMvcGiris.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.ViewComponents
{
    public class KategoriList : ViewComponent
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriList(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public IViewComponentResult Invoke()
        {            
            return View(_kategoriRepository.HepsiniGetir());
        }
    }
}
