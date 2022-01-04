using ApartmentsApp.Services.BillServices;
using ApartmentsApp.Services.BillServices.CustomBill;
using ApartmentsApp.Services.HomeServices;
using ApartmentsApp.Services.MessageServices;
using ApartmentsApp.Services.UserServices;
using ApartmentsApp.WebUI.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApartmentsApp.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //mapping oluþturma
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            //mapper objesini inject etmek için interfaceden kendi classýmýzdan oluþturduðumuz objeyi map için create ediyoruz.
            IMapper mapper = _mappingProfile.CreateMapper();
            //mapperý inject ediyoruz.
            services.AddSingleton(mapper);

            //servislerdeki dependency injection tanýmlamlarý
            services.AddScoped<IBillService, BillManager>();
            services.AddScoped<ICustomBillService, CustomBillManager>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IUserService, UserManager>();

            services.AddControllersWithViews();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

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
            app.UseSpaStaticFiles();

            app.UseRouting();

            //identity için
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            //single page application. yani react için kurulum kýsmý
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
