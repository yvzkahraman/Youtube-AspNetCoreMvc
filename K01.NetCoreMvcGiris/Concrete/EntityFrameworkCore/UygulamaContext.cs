using K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore.Mapping;
using K01.NetCoreMvcGiris.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore
{
    public class UygulamaContext :DbContext
    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLExpress; database=YoutubeUyg01; user id=sa; password=1;"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Musteri>().Property(I => I.Ad).HasMaxLength(100);

            //modelBuilder.ApplyConfiguration();
          
            modelBuilder.ApplyConfiguration(new UrunMap());
            modelBuilder.ApplyConfiguration(new KategoriMap());
            modelBuilder.ApplyConfiguration(new MusteriMap());


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }

        

    }
}
