using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Extensions;
using K01.NetCoreMvcGiris.Interfaces;
using K01.NetCoreMvcGiris.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMusteriRepository _musteriRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IUrunRepository _urunRepository;
        private readonly SignInManager<UygKullanici> _signInManager;
        readonly UserManager<UygKullanici> _userManager;

        //Cokiee


        public HomeController(
            IMusteriRepository musteriRepository,
            IKategoriRepository kategoriRepository,
            IUrunRepository urunRepository,
            UserManager<UygKullanici> userManager,
            SignInManager<UygKullanici> signInManager)


        {
            _userManager = userManager;
            _musteriRepository = musteriRepository;
            _kategoriRepository = kategoriRepository;
            _urunRepository = urunRepository;
            _signInManager = signInManager;
        }
        public IActionResult UrunDetay(int id)
        {
            ViewBag.Sepet = HttpContext.Session.GetObject<List<SepetModel>>("sepet");
            return View(_urunRepository.IdileGetir(id));
        }


        public IActionResult Index(int aktifSayfa = 1)
        {
            // 30 
            ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_urunRepository.HepsiniGetir().Count / 28);
            ViewBag.AktifSayfa = aktifSayfa;

            ViewBag.Sepet = HttpContext.Session.GetObject<List<SepetModel>>("sepet");

            return View(_urunRepository.HepsiniGetir().OrderByDescending(I => I.Id).ToList().Skip((aktifSayfa - 1) * 28).Take(28).ToList());
        }


        public IActionResult SepeteEkle(int id)
        {
            List<SepetModel> urunler = HttpContext.Session.GetObject<List<SepetModel>>("sepet");
            if (urunler == null)
            {
                urunler = new List<SepetModel>();
            }

            var eklenecekUrun = _urunRepository.IdileGetir(id);

            SepetModel model = new SepetModel
            {
                Ad = eklenecekUrun.Ad,
                Id = eklenecekUrun.Id,
                Fiyat = eklenecekUrun.Fiyat
            };
            urunler.Add(model);
            HttpContext.Session.SetObject("sepet", urunler);

            return RedirectToAction("Index", "Home");
         }


        #region Kullanici Islemleri

        public IActionResult YeniKayit()
        {
            ViewBag.Sepet = HttpContext.Session.GetObject<List<SepetModel>>("sepet");
            return View(new KullaniciViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> YeniKayit(KullaniciViewModel model)
        {
            if (ModelState.IsValid)
            {
                // md5 sha256

                UygKullanici kullanici = new UygKullanici();
                kullanici.UserName = model.KullaniciAd;
                kullanici.Email = model.Email;
                kullanici.PhoneNumber = model.Telefon;


                var sonuc = await _userManager.CreateAsync(kullanici, model.Sifre);

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("GirisYap", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", "Hata");
                    foreach (IdentityError hata in sonuc.Errors)
                    {
                        ModelState.AddModelError("", hata.Description);
                    }
                }

            }

            return View(model);
        }


        public IActionResult GirisYap()
        {
            ViewBag.Sepet = HttpContext.Session.GetObject<List<SepetModel>>("sepet");
            return View(new KullaniciGirisViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(KullaniciGirisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gelenKullanici = await _userManager.FindByNameAsync(model.KullaniciAd);
                if (gelenKullanici != null)
                {
                    var sonuc = await _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre, model.BeniHatirla, false);

                    if (sonuc.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "kullanıcı adınız veya şifreniz hatalı");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "kullanıcı adınız veya şifreniz hatalı");
                }
            }

            return View(model);
        }
        //public async void Kaydet()
        //{

        //}

        #endregion
    }
}