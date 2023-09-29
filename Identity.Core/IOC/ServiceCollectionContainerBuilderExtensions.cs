using Identity.Core.Helpers.Abstract;
using Identity.Core.Helpers.Concrete;
using Identity.Data.CustomValidations;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.Core.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizeHelper, AuthorizeHelper>();
            services.AddScoped<IUserHelper, UserHelper>();

            return services;
        }

        public static IServiceCollection AddIdentity<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();

            if (serviceProvider is null)
                throw new NullReferenceException(nameof(serviceProvider));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5; //En az kaç karakterli olması gerektiğini belirtiyoruz.
                options.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluğunu kaldırıyoruz.
                options.Password.RequireLowercase = false; //Küçük harf zorunluluğunu kaldırıyoruz.
                options.Password.RequireUppercase = false; //Büyük harf zorunluluğunu kaldırıyoruz.
                options.Password.RequireDigit = false; //0-9 arası sayısal karakter zorunluluğunu kaldırıyoruz.

                options.Lockout.MaxFailedAccessAttempts = 5;
                /*
                 Identity mekanizmasında UserName alanı değiştirilemez bir şekilde default olarak unique(tekil)tir.
                 Identity mekanizmasında tüm varsayımsal ayarlar opsiyonel olarak değiştirilebilir değildir.
                 */

                options.User.RequireUniqueEmail = true; //Email adreslerini tekilleştiriyoruz.Normalde Identity'de tekil olma zorunluluğu yok.
                options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; //Kullanıcı adında geçerli olan karakterleri belirtiyoruz.Türkçe karakter dahil.


            }).AddPasswordValidator<PasswordValidation>()
              .AddUserValidator<UserValidation>()
              .AddErrorDescriber<IdentityErrorDescriber>()
              .AddEntityFrameworkStores<TContext>() // Bilgilerin nereye kaydedileceğini addentityframeworkstores diyerek bulabiliriz.
              .AddDefaultTokenProviders(); // Json web tokenı kendin yazmak istediğin zaman burayı kaldırabilirisn.Aksi halde otomatik bir json web token mantığını kullanmak istiyorsan bunu eklemelisin.

            services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = new PathString("/Authentication/Logout");
                options.LoginPath = new PathString("/Authentication/Login");
                options.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityExampleCookie", //Oluşturulacak Cookie'yi isimlendiriyoruz.
                    HttpOnly = false, //Kötü niyetli insanların client-side tarafından Cookie'ye erişmesini engelliyoruz.
                    //Expiration = TimeSpan.FromMinutes(20), //Oluşturulacak Cookie'nin vadesini belirliyoruz.
                    SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
                    SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden erişilebilir yapıyoruz.
                };
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = false; //Expiration süresinin yarısı kadar süre zarfında istekte bulunulursa eğer geri kalan yarısını tekrar sıfırlayarak ilk ayarlanan süreyi tazeleyecektir.
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20); //CookieBuilder nesnesinde tanımlanan Expiration değerinin varsayılan değerlerle ezilme ihtimaline karşın tekrardan Cookie vadesi burada da belirtiliyor.
            });


            return services;
        }
    }
}
