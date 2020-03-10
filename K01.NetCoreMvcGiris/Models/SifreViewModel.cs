using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Models
{
    public class SifreViewModel
    {
        [Required(ErrorMessage = "Eski Sifre alanı gereklidir")]
        public string EskiSifre { get; set; }
        [Required(ErrorMessage ="Sifre alanı gereklidir")]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "Sifre tekrar alanı gereklidir")]
        [Compare("Sifre",ErrorMessage ="Şifreler uyuşmuyor")]
        public string SifreTekrar { get; set; }
    }
}
