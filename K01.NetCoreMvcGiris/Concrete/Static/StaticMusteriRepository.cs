using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace K01.NetCoreMvcGiris.Concrete.Static
{
    //public class StaticMusteriRepository : IMusteriRepository
    //{
    //    public  List<Musteri> MusterileriGetir()
    //    {
    //        return StaticDb.musteriler;
    //    }

    //    public  Musteri BirMusteriGetir(int id)
    //    {
    //        return StaticDb.musteriler.Find(I => I.Id == id);
    //    }

    //    public  void MusteriEkle(Musteri musteri)
    //    {
    //        var sonMusteri = StaticDb.musteriler.Last();
    //        int id = sonMusteri.Id;
    //        musteri.Id = (id + 1);
    //        StaticDb.musteriler.Add(musteri);
    //    }

    //    public  Musteri IdileMusteriGetir(int id)
    //    {
    //        return StaticDb.musteriler.Where(I => I.Id == id).FirstOrDefault();
    //    }

    //    public  void MusteriGuncelle(Musteri musteri)
    //    {
    //        var bulunanMusteri = StaticDb.musteriler.Where(I => I.Id == musteri.Id).FirstOrDefault();
    //        if (bulunanMusteri != null)
    //        {
    //            int index = StaticDb.musteriler.IndexOf(bulunanMusteri);
    //            StaticDb.musteriler[index].Ad = musteri.Ad;
    //        }


    //    }

    //    public void MusteriSil(Musteri musteri)
    //    {
    //        StaticDb.musteriler.Remove(musteri);
    //    }
    //}
}
