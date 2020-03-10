using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Interfaces
{
    interface IDeneme
    {
    }

    public interface ISingleton
    {
        int Sayi { get; set; }
        int RastgeleSayiGetir();
    }
    public interface IScoped
    {
        int Sayi { get; set; }
        int RastgeleSayiGetir();
    }
    public interface ITransient
    {
        int Sayi { get; set; }
        int RastgeleSayiGetir();
    }
}
