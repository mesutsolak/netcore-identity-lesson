using Identity.Entities;
using Identity.Entities.Models;
using Identity.WebUI.CustomValidations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

                /*
                 Identity mekanizmasında UserName alanı değiştirilemez bir şekilde default olarak unique(tekil)tir.
                 Identity mekanizmasında tüm varsayımsal ayarlar opsiyonel olarak değiştirilebilir değildir.
                 */

                options.User.RequireUniqueEmail = true; //Email adreslerini tekilleştiriyoruz.Normalde Identity'de tekil olma zorunluluğu yok.
                options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; //Kullanıcı adında geçerli olan karakterleri belirtiyoruz.Türkçe karakter dahil.


            }).AddPasswordValidator<CustomPasswordValidation>()
              .AddUserValidator<CustomUserValidation>()
              .AddErrorDescriber<CustomIdentityErrorDescriber>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

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
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
