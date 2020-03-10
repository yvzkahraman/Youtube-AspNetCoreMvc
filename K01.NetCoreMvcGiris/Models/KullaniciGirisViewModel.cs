using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Models
{
    public class KullaniciGirisViewModel
    {
        
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Sifre { get; set; }
        public bool BeniHatirla { get; set; }
    }
}
