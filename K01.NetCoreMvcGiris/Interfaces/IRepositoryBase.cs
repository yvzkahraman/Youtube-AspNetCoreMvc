using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Interfaces
{
    public interface IRepositoryBase<T> 
        where T: class,ITable,new()
    {
        public List<T> HepsiniGetir();
        public T  IdileGetir(int id);
        public void Ekle(T entity);
        public void Guncelle(T entity);
        public void Sil(T entity);
    }
}
