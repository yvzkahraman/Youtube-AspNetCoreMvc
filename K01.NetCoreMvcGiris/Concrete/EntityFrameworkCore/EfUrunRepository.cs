using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore
{
    public class EfUrunRepository : EFRepositoryBase<Urun>, IUrunRepository
    {
    }
}
