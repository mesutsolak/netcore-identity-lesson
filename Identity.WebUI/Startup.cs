using Identity.Entities;
using Identity.Entities.Models;
using Identity.WebUI.CustomValidations;
using Identity.WebUI.Security.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Identity.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ConfigureServices metot eklerken sırasıyla eklenmesi oldukça önemlidir.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityExample")));

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


            }).AddPasswordValidator<CustomPasswordValidation>()
              .AddUserValidator<CustomUserValidation>()
              .AddErrorDescriber<CustomIdentityErrorDescriber>()
              .AddEntityFrameworkStores<ApplicationDbContext>() // Bilgilerin nereye kaydedileceğini addentityframeworkstores diyerek bulabiliriz.
              .AddDefaultTokenProviders(); // Json web tokenı kendin yazmak istediğin zaman burayı kaldırabilirisn.Aksi halde otomatik bir json web token mantığını kullanmak istiyorsan bunu eklemelisin.

            /*
             * 
             *  Default şemalar belirtiyoruz çünkü farklı şemalarda da login olma işlemlerini gerçekleştirebilirsin.
                Uygulamanın kendi authentication şemasını aşağıdaki şekilde belirtebiliyoruz.
                Ayrıca token bazlı authentication şemasıda belirtmeliyiz.Bunun nedeni uygulamanın şeması ile
                token authentication şemasının aynı olmasını sağlamaktır.Aynı olmalıki üyelik sistemi ile json web token beraber
                çalışabilsinler.
             */

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //Krıtik parametre

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
            {
                // Token bazlı authentication şeması için
                services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOptions"));
                var tokenOptions = Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();


                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = new PathString("/User/Logout");
                options.LoginPath = new PathString("/User/Login");
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

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                                             pattern: "{controller=User}/{action=Home}/{id?}");

            });
        }
    }
}
