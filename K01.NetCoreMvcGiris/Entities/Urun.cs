using K01.NetCoreMvcGiris.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Entities
{
    public class Urun : ITable
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ad boş geçilemez")]
        public string Ad { get; set; }
        //[Column(TypeName ="ntext")]
        [Required(ErrorMessage ="Aciklama boş geçilemez")]
        public string Aciklama { get; set; }
        [Required(ErrorMessage = "Fiyat boş geçilemez")]
        public decimal Fiyat { get; set; }
     
        public string ResimUrl { get; set; }
        //[ForeignKey("Kategori")]
        [Required(ErrorMessage = "Lütfen bir kategori seçiniz")]
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        //kritik
        // max
        // stok

        [NotMapped]
        public IFormFile Resim { get; set; }
    }
}
