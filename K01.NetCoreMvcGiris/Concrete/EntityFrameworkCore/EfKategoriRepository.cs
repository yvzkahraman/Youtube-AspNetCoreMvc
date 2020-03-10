using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore
{
    public class EfKategoriRepository : EFRepositoryBase<Kategori>, IKategoriRepository
    {
        public void Yavuz()
        {
            //kategoriye özel metod
        }
    }
}
