using System.Linq;
using System.Threading.Tasks;
using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace K01.NetCoreMvcGiris.Controllers
{
    public class AdminController : Controller
    {
        readonly UserManager<UygKullanici> _userManager;
        readonly SignInManager<UygKullanici> _signInManager;
        readonly RoleManager<UygRol> _roleManager;

        public AdminController(UserManager<UygKullanici> userManager, SignInManager<UygKullanici> signInManager, RoleManager<UygRol> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [Authorize]
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

        public IActionResult KullaniciList()
        {
            return View(_userManager.Users.ToList());
        }

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
                RolUpdateViewModel model = new RolUpdateViewModel
                {
                    Id = gosterilecekRol.Id,
                    Name = gosterilecekRol.Name
                };
                return View(model);
            }
            return RedirectToAction("Roller", "Admin");
        }

        [HttpPost]
        public IActionResult RolGuncelle(RolUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekRol = _roleManager.FindByIdAsync(model.Id).Result;
                guncellenecekRol.Name = model.Name;

                var sonuc = _roleManager.UpdateAsync(guncellenecekRol).Result;
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

        #endregion
        public IActionResult CikisYap()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}