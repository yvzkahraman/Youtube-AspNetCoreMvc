using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using K01.NetCoreMvcGiris.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
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

        public IActionResult Index(int aktifSayfa = 1)
        {
            // 30 
            ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_urunRepository.HepsiniGetir().Count / 28);
            ViewBag.AktifSayfa = aktifSayfa;

            return View(_urunRepository.HepsiniGetir().OrderByDescending(I => I.Id).ToList().Skip((aktifSayfa - 1) * 28).Take(28).ToList());
        }

        #region Müşteri İşlemleri
        public IActionResult MusteriList()
        {
            var musteriler = _musteriRepository.HepsiniGetir();
            //ViewBag.Kategoriler = StaticDb.KateogrileriGetir();
            return View(musteriler);
        }
        public IActionResult MusteriSil(int id)
        {
            var silinecek = _musteriRepository.IdileGetir(id);
            _musteriRepository.Sil(silinecek);
            return RedirectToAction("MusteriList");
        }

        public IActionResult Detay(int id)
        {
            var musteri = _musteriRepository.IdileGetir(id);
            return View(musteri);
        }

        public IActionResult MusteriEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MusteriEklePost()
        {
            var isim = HttpContext.Request.Form["isim"].ToString();

            _musteriRepository.Ekle(new Musteri()
            {
                Ad = isim
            });

            return RedirectToAction("Index");
        }


        public IActionResult MusteriDuzenle(int id)
        {
            return View(_musteriRepository.IdileGetir(id));
        }

        [HttpPost]
        public IActionResult MusteriDuzenle(Musteri musteri)
        {
            _musteriRepository.Guncelle(musteri);
            return RedirectToAction("Index");
        }



        #endregion

        #region Kategori İşlemleri

        public IActionResult KategoriEkle()
        {
            return View(new Kategori());
        }

        [HttpPost]
        public IActionResult KategoriEkle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _kategoriRepository.Ekle(kategori);
                return RedirectToAction("KategoriList");
            }
            else
            {
                return View(kategori);
            }

        }

        public IActionResult KategoriSil(int id)
        {
            var silinecek = _kategoriRepository.IdileGetir(id);
            _kategoriRepository.Sil(silinecek);
            return RedirectToAction("KategoriList");
        }

        public IActionResult KategoriList()
        {
            return View(_kategoriRepository.HepsiniGetir());
        }

        public IActionResult KategoriDuzenle(int id)
        {
            return View(_kategoriRepository.IdileGetir(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult KategoriDuzenle(Kategori kategori)
        {
            _kategoriRepository.Guncelle(kategori);
            return RedirectToAction("KategoriList");
        }

        #endregion

        #region Ürün İşlemleri
        public IActionResult UrunList(int aktifSayfa = 1)
        {

            ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_urunRepository.HepsiniGetir().Count / 28);
            ViewBag.AktifSayfa = aktifSayfa;

            return View(_urunRepository.HepsiniGetir().OrderByDescending(I => I.Id).ToList().Skip((aktifSayfa - 1) * 28).Take(28).ToList());
        }

        public IActionResult UrunSil(int id)
        {
            _urunRepository.Sil(new Urun()
            {
                Id = id
            });
            return RedirectToAction("UrunList");
        }

        public IActionResult UrunEkle()
        {
            ViewBag.Kategoriler = new SelectList(_kategoriRepository.HepsiniGetir(), "Id", "Ad");
            return View(new Urun());
        }

        [HttpPost]
        public IActionResult UrunEkle(Urun urun)
        {
            if (ModelState.IsValid)
            {
                if (urun.Resim != null)
                {
                    //fiziksel
                    // yolum/wwwroot/img/ad.jpg                   
                    string ad = Guid.NewGuid() + Path.GetExtension(urun.Resim.FileName);
                    string fizikselAdres = Directory.GetCurrentDirectory();
                    string kaydedilecekYer = "wwwroot/img/";
                    //Path.Combine()
                    string path = fizikselAdres + "/" + kaydedilecekYer + "/" + ad;

                    //urun.Resim.CopyTo()

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        urun.Resim.CopyTo(stream);
                    }
                    urun.ResimUrl = ad;
                }
                //ekleme işlemleri
                _urunRepository.Ekle(urun);
                return RedirectToAction("UrunList");
            }
            else
            {
                return View(urun);
            }
        }


        public IActionResult UrunDuzenle(int id)
        {
            var gelenUrun = _urunRepository.IdileGetir(id);
            var gelenKategori = _kategoriRepository.IdileGetir(gelenUrun.KategoriId);

            ViewBag.Kategoriler = new SelectList(_kategoriRepository.HepsiniGetir(), "Id", "Ad", gelenKategori.Ad);

            return View(gelenUrun);
        }

        public IActionResult UrunDetay(int id)
        {
            return View(_urunRepository.IdileGetir(id));
        }

        [HttpPost]
        public IActionResult UrunDuzenle(Urun urun)
        {

            if (ModelState.IsValid)
            {
                if (urun.Resim != null)
                {
                    //fiziksel
                    // yolum/wwwroot/img/ad.jpg
                    string ad = Guid.NewGuid() + Path.GetExtension(urun.Resim.FileName);
                    string fizikselAdres = Directory.GetCurrentDirectory();
                    string kaydedilecekYer = "wwwroot/img/";
                    //Path.Combine()
                    string path = fizikselAdres + "/" + kaydedilecekYer + "/" + ad;

                    //urun.Resim.CopyTo()

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        urun.Resim.CopyTo(stream);
                    }
                    urun.ResimUrl = ad;
                }
                //ekleme işlemleri
                _urunRepository.Guncelle(urun);
                return RedirectToAction("UrunList");
            }
            else
            {
                return View(urun);
            }
        }


        #endregion

        #region Kullanici Islemleri

        public IActionResult YeniKayit()
        {
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