using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Entities
{
    public class Musteri : ITable
    {
        public int Id { get; set; }
      
        public string Ad { get; set; }
    }
}
