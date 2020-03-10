using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.Static
{
    public class Deneme
    {
    }

    public class TestSingleton : ISingleton
    {
        public TestSingleton()
        {
            Sayi = RastgeleSayiGetir();
        }

        public int Sayi { get; set; }

        public int RastgeleSayiGetir()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }

    public class TestTransient : ITransient
    {
        public TestTransient()
        {
            Sayi = RastgeleSayiGetir();
        }

        public int Sayi { get ; set ; }

        public int RastgeleSayiGetir()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }

    public class TestScoped : IScoped
    {
        public TestScoped()
        {
            Sayi = RastgeleSayiGetir();
        }

        public int Sayi { get; set; }

        public int RastgeleSayiGetir()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
}
