using K01.NetCoreMvcGiris.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore
{
    public class EfIdentityContext : IdentityDbContext<UygKullanici,UygRol,string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLExpress; database=YoutubeUyg01; user id=sa; password=1;");

        }
    }
}
