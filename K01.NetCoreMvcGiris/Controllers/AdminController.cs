using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using K01.NetCoreMvcGiris.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace K01.NetCoreMvcGiris.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly UserManager<UygKullanici> _userManager;
        readonly SignInManager<UygKullanici> _signInManager;
        readonly RoleManager<UygRol> _roleManager;
        readonly IMusteriRepository _musteriRepository;
        readonly IKategoriRepository _kategoriRepository;
        readonly IUrunRepository _urunRepository;

        public AdminController(UserManager<UygKullanici> userManager, SignInManager<UygKullanici> signInManager, RoleManager<UygRol> roleManager, IMusteriRepository musteriRepository, IKategoriRepository kategoriRepository, IUrunRepository urunRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _musteriRepository = musteriRepository;
            _kategoriRepository = kategoriRepository;
            _urunRepository = urunRepository;
        }

       
        public IActionResult Index()
        {
            var kullanciAd = User.Identity.Name;
            var kullanici = _userManager.FindByNameAsync(kullanciAd).Result;
            KullaniciViewModel model = new KullaniciViewModel
            {
                Email = kullanici.Email,
                KullaniciAd = kullanici.UserName,
                Telefon = kullanici.PhoneNumber
            };
            return View(model);
        }

        #region Kullanici Islemleri
        [Authorize]
        public IActionResult SifreDegistir()
        {
            return View(new SifreViewModel());
        }

        [HttpPost]
        public IActionResult SifreDegistir(SifreViewModel model)
        {

            if (ModelState.IsValid)
            {
                var kullanici = _userManager.FindByNameAsync(User.Identity.Name).Result;
                //var sifresiDogrumu= _userManager.CheckPasswordAsync(kullanici, model.EskiSifre).Result;
                //if (sifresiDogrumu)
                //{
                var sonuc = _userManager.ChangePasswordAsync(kullanici, model.EskiSifre, model.Sifre).Result;

                if (sonuc.Succeeded)
                {
                    TempData["islem"] = "Şifreniz başarı ile değiştirildi";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var item in sonuc.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Eski şifreniz doğru değil");
                //}

            }

            return View(model);
        }

        [Authorize(Roles="Admin")]
        public IActionResult KullaniciList()
        {
            return View(_userManager.Users.ToList());
        }

        [Authorize(Roles="Admin")]
        public IActionResult KullaniciGuncelle()
        {
            var kullanici = _userManager.FindByNameAsync(User.Identity.Name).Result;
            KullaniciViewModel model = new KullaniciViewModel
            {
                Email = kullanici.Email,
                KullaniciAd = kullanici.UserName,
                Telefon = kullanici.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciGuncelle(KullaniciViewModel model)
        {
            ModelState.Remove(nameof(KullaniciViewModel.Sifre));
            if (ModelState.IsValid)
            {
                var kullanici = _userManager.FindByNameAsync(User.Identity.Name).Result;
                kullanici.Email = model.Email;
                kullanici.UserName = model.KullaniciAd;
                kullanici.PhoneNumber = model.Telefon;

                var sonuc = _userManager.UpdateAsync(kullanici).Result;
                if (sonuc.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(kullanici);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(kullanici, true);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var error in sonuc.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        #endregion

        #region Kategori İşlemleri
        [Authorize(Roles = "Admin,Icerik")]
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
        [Authorize(Roles = "Admin,Icerik")]
        public IActionResult KategoriSil(int id)
        {
            var silinecek = _kategoriRepository.IdileGetir(id);
            _kategoriRepository.Sil(silinecek);
            return RedirectToAction("KategoriList");
        }
        [Authorize(Roles = "Admin,Icerik")]
        public IActionResult KategoriList()
        {
            return View(_kategoriRepository.HepsiniGetir());
        }
        [Authorize(Roles = "Admin,Icerik")]
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
        [Authorize(Roles = "Admin,Icerik")]
        public IActionResult UrunList(int aktifSayfa = 1)
        {

            ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_urunRepository.HepsiniGetir().Count / 28);
            ViewBag.AktifSayfa = aktifSayfa;

            return View(_urunRepository.HepsiniGetir().OrderByDescending(I => I.Id).ToList().Skip((aktifSayfa - 1) * 28).Take(28).ToList());
        }
        [Authorize(Roles = "Admin,Icerik")]
        public IActionResult UrunSil(int id)
        {
            _urunRepository.Sil(new Urun()
            {
                Id = id
            });
            return RedirectToAction("UrunList");
        }
        [Authorize(Roles = "Admin,Icerik")]
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

        [Authorize(Roles = "Admin,Icerik")]
        public IActionResult UrunDuzenle(int id)
        {
            var gelenUrun = _urunRepository.IdileGetir(id);
            var gelenKategori = _kategoriRepository.IdileGetir(gelenUrun.KategoriId);

            ViewBag.Kategoriler = new SelectList(_kategoriRepository.HepsiniGetir(), "Id", "Ad", gelenKategori.Ad);

            return View(gelenUrun);
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


        #region Müşteri İşlemleri
        [Authorize(Roles = "Admin")]
        public IActionResult MusteriList()
        {
            var musteriler = _musteriRepository.HepsiniGetir();
            //ViewBag.Kategoriler = StaticDb.KateogrileriGetir();
            return View(musteriler);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult MusteriSil(int id)
        {
            var silinecek = _musteriRepository.IdileGetir(id);
            _musteriRepository.Sil(silinecek);
            return RedirectToAction("MusteriList");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Detay(int id)
        {
            var musteri = _musteriRepository.IdileGetir(id);
            return View(musteri);
        }
        [Authorize(Roles = "Admin")]
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

            return RedirectToAction("MusteriList");
        }

        [Authorize(Roles = "Admin")]
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


        #region Role Islemleri

        public IActionResult Roller()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult RolEkle()
        {
            return View(new RolModel());
        }

        [HttpPost]
        public IActionResult RolEkle(RolModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = _roleManager.CreateAsync(new UygRol()
                {
                    Name = model.Name
                }).Result;

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("Roller", "Admin");
                }
                else
                {
                    foreach (var item in sonuc.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }


        public IActionResult RolGuncelle(string id)
        {
            var gosterilecekRol = _roleManager.FindByIdAsync(id).Result;
            if (gosterilecekRol != null)
            {
                RoleUpdateViewModel model = new RoleUpdateViewModel
                {
                    Id = gosterilecekRol.Id,
                    Name = gosterilecekRol.Name
                };
                return View(model);
            }
            return RedirectToAction("Roller", "Index");
        }

        [HttpPost]
        public IActionResult RolGuncelle(RoleUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekRol = _roleManager.FindByIdAsync(model.Id).Result;
                guncellenecekRol.Name = model.Name;
                var sonuc= _roleManager.UpdateAsync(guncellenecekRol).Result;
                if (sonuc.Succeeded)
                {
                    return RedirectToAction("Roller", "Admin");
                }
                else
                {
                    foreach (var item in sonuc.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }


        public IActionResult RolSil(string id)
        {
            var silinecekRol = _roleManager.FindByIdAsync(id).Result;
            if (silinecekRol != null)
            {
                string hatalar = string.Empty;
                var sonuc = _roleManager.DeleteAsync(silinecekRol).Result;
                if (sonuc.Succeeded)
                {
                    return RedirectToAction("Roller", "Admin");
                }
                else
                {
                    foreach (var item in sonuc.Errors)
                    {
                        hatalar += " " + item.Description;
                    }
                    TempData["hata"] = hatalar;
                }
            }
            return RedirectToAction("Roller", "Admin");
        }

        public IActionResult RolAta(string kullaniciId)
        {
            var bulunanKullanici= _userManager.FindByIdAsync(kullaniciId).Result;
            if (bulunanKullanici != null)
            {
                TempData["kullaniciId"] = bulunanKullanici.Id;
                var roller = _roleManager.Roles.ToList();
                var kullaniciRoller = _userManager.GetRolesAsync(bulunanKullanici).Result.ToList();

                List<RolAtaViewModel> models = new List<RolAtaViewModel>();
                foreach (var rol in roller)
                {
                    RolAtaViewModel model = new RolAtaViewModel();
                    model.RolId = rol.Id;
                    model.RolAd = rol.Name;
                    model.VarMi = kullaniciRoller.Contains(rol.Name);

                    models.Add(model);
                }

                return View(models);
            }
           

            return RedirectToAction("KullaniciList","Admin");
        }

        [HttpPost]
        public async Task<IActionResult> RolAta(List<RolAtaViewModel> model)
        {
            string kullaniciId = TempData["kullaniciId"].ToString();
            var kullanici = _userManager.FindByIdAsync(kullaniciId).Result;
            
            foreach (var item in model)
            {
                if (item.VarMi)
                {
                   await _userManager.AddToRoleAsync(kullanici, item.RolAd);
                }
                else
                {
                   await _userManager.RemoveFromRoleAsync(kullanici, item.RolAd);
                }
            }
            return RedirectToAction("KullaniciList","Admin");
        }

        #endregion
        public IActionResult CikisYap()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult YetkiDusuk()
        {
            return View();
        }
    }
}