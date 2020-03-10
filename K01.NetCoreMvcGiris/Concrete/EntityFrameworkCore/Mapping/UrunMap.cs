using K01.NetCoreMvcGiris.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore.Mapping
{
    public class UrunMap : IEntityTypeConfiguration<Urun>
    {
        public void Configure(EntityTypeBuilder<Urun> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).ValueGeneratedOnAdd();

            builder.Property(I => I.Aciklama).HasColumnType("ntext");
            builder.Property(I => I.Ad).HasMaxLength(100).IsRequired(true);
            builder.Property(I => I.ResimUrl).HasMaxLength(300);

            builder.HasOne(I => I.Kategori).WithMany(I => I.Urunler).HasForeignKey(I => I.KategoriId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
