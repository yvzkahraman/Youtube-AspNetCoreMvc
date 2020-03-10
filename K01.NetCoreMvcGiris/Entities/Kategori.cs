using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Entities
{
    public class Kategori : ITable
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Ad { get; set; }

        public List<Urun> Urunler { get; set; }

    }
}
