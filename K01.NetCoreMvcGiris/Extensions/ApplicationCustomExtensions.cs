using K01.NetCoreMvcGiris.Concrete.EntityFrameworkCore;
using K01.NetCoreMvcGiris.Concrete.Static;
using K01.NetCoreMvcGiris.CustomIdentityValidation;
using K01.NetCoreMvcGiris.Entities;
using K01.NetCoreMvcGiris.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace K01.NetCoreMvcGiris.Extensions
{
    public static class ApplicationCustomExtensions
    {
        public static IApplicationBuilder UseMyRouting(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                //.com/spor/takimadi/yeni-transfer

                // routes.MapRoute(
                //name: "dilBilgiRoute",
                //template: "programlama/{dil}",
                //constraints: new { dil = new Programlama() },
                //defaults: new { controller = "User", action = "Index" }
                //);


                // routes.MapRoute(
                //      name: "kisiBilgiRoute",
                //      template: "kisiBilgi/{kisiBilgi}",
                //      constraints:new {kisiBilgi= new BoolRouteConstraint()},
                //      defaults: new { controller = "User", action = "KisiBilgi" }
                //      );



                // routes.MapRoute(
                //    name: "urunDetayRoute",
                //    template: "urunDetay/{id:int}",
                //    defaults: new { controller = "Home", action = "UrunDetay" }
                //    );


                // routes.MapRoute(
                //    name: "anasayfaRoute",
                //    template: "anasayfa",
                //    defaults: new { controller = "Home", action = "Index" }
                //    );


                //Home/Index/id?
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    );

            });

            return app;
        }

        public static IApplicationBuilder UseCustomStaticFiles(this IApplicationBuilder app)
        {

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/content"
            });
             

            return app;
        }

        public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
        {
            services.AddScoped<IKategoriRepository, EfKategoriRepository>();
            services.AddScoped<IMusteriRepository, EFMusteriRepository>();
            services.AddScoped<IUrunRepository, EfUrunRepository>();


            services.AddSingleton<ISingleton, TestSingleton>();
            services.AddScoped<IScoped, TestScoped>();
            services.AddTransient<ITransient, TestTransient>();

            return services;
        }
       

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {

            services.AddDbContext<EfIdentityContext>();



            services.AddIdentity<UygKullanici, UygRol>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                              
            })
                .AddErrorDescriber<CustomValidation>()
                .AddEntityFrameworkStores<EfIdentityContext>();


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/GirisYap/");
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.Cookie.Name = "MyCookie";

            });


            return services;
        }
    }
}
