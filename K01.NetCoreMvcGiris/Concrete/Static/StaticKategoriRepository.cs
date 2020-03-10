using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace K01.NetCoreMvcGiris.Concrete.Static
{
    //public class StaticKategoriRepository : IKategoriRepository
    //{
    //    public  List<Kategori> KateogrileriGetir()
    //    {
    //        return StaticDb.kategoriler;
    //    }

    //    public  Kategori BirKategoriGetirIdile(int id)
    //    {
    //        return StaticDb.kategoriler.Find(I => I.Id == id);
    //    }

    //    public  void KategoriEkle(Kategori kategori)
    //    {
    //        var sonKategori = StaticDb.kategoriler.Last();
    //        int id = sonKategori.Id;
    //        kategori.Id = (id + 1);
    //        StaticDb.kategoriler.Add(kategori);
    //    }

    //    public  void KategoriGuncelle(Kategori kategori)
    //    {
    //        var bulunanKategori = StaticDb.kategoriler.Where(I => I.Id == kategori.Id).FirstOrDefault();
    //        if (bulunanKategori != null)
    //        {
    //            int index = StaticDb.kategoriler.IndexOf(bulunanKategori);
    //            StaticDb.kategoriler[index].Ad = kategori.Ad;
    //        }
    //    }

    //    public void KategoriSil(Kategori kategori)
    //    {
    //        StaticDb.kategoriler.Remove(kategori);
    //    }
    //}
}
