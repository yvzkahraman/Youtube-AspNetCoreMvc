using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace K01.NetCoreMvcGiris.Concrete.Static
{
    //public class StaticUrunRepository : IUrunRepository
    //{

    //    public List<Urun> UrunleriListele()
    //    {
    //        return StaticDb.urunler;
    //    }

    //    public void UrunEkle(Urun urun)
    //    {
    //        var sonUrun = StaticDb.urunler.Last();
    //        int id = sonUrun.Id;
    //        urun.Id = (id + 1);
    //        StaticDb.urunler.Add(urun);
    //    }

    //    public Urun UrunGetirId(int id)
    //    {
    //        return StaticDb.urunler.Find(I => I.Id == id);
    //    }

    //    public void UrunSil(int id)
    //    {
    //        StaticDb.urunler.Remove(UrunGetirId(id));
    //    }

    //    public void UrunGuncelle(Urun urun)
    //    {
    //        var bulunanUrun = StaticDb.urunler.Where(I => I.Id == urun.Id).FirstOrDefault();
    //        if (bulunanUrun != null)
    //        {
    //            int index = StaticDb.urunler.IndexOf(bulunanUrun);
    //            StaticDb.urunler[index].Ad = urun.Ad;
    //            StaticDb.urunler[index].Aciklama = urun.Aciklama;
    //            StaticDb.urunler[index].Fiyat = urun.Fiyat;
    //            StaticDb.urunler[index].KategoriId = urun.KategoriId;
    //            StaticDb.urunler[index].ResimUrl = urun.ResimUrl;
    //        }
    //    }
    //}
}
