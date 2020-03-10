using System.Linq;
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
        
        public AdminController(UserManager<UygKullanici> userManager, SignInManager<UygKullanici> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
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
                var sonuc =_userManager.ChangePasswordAsync(kullanici, model.EskiSifre, model.Sifre).Result;

                if (sonuc.Succeeded)
                {
                    TempData["islem"] = "Şifreniz başarı ile değiştirildi";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var item in sonuc.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
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




        public IActionResult CikisYap()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}