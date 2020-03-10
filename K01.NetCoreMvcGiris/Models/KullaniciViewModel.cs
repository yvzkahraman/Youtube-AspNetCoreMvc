using System.ComponentModel.DataAnnotations;

namespace K01.NetCoreMvcGiris.Models
{
    public class KullaniciViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı zorunlu")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Sifre zorunlu")]
        public string Sifre { get; set; }

        public string Telefon { get; set; }
        [Required(ErrorMessage = "Email zorunlu")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Tanımlı bir email adresi giriniz...")]
        public string Email { get; set; }
    }
}
