using K01.NetCoreMvcGiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore
{
    public class EFRepositoryBase<T> : IRepositoryBase<T>
        where T : class, ITable, new()
    {
        public void Ekle(T entity)
        {
            using var context = new UygulamaContext();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Guncelle(T entity)
        {
            using var context = new UygulamaContext();
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public List<T> HepsiniGetir()
        {
            using var context = new UygulamaContext();
            return context.Set<T>().ToList();

        }

        public T IdileGetir(int id)
        {
            using var context = new UygulamaContext();
            return context.Set<T>().Find(id);
        }

        public void Sil(T entity)
        {
            using var context = new UygulamaContext();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
    }
}
