using K01.NetCoreMvcGiris.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore.Mapping
{
    public class KategoriMap : IEntityTypeConfiguration<Kategori>
    {
        public void Configure(EntityTypeBuilder<Kategori> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).ValueGeneratedOnAdd();

            builder.Property(I => I.Ad).HasMaxLength(100).IsRequired(true);
        }
    }
}
