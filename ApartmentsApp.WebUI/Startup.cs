using ApartmentsApp.Services.BillServices;
using ApartmentsApp.Services.BillServices.CustomBill;
using ApartmentsApp.Services.HomeServices;
using ApartmentsApp.Services.MessageServices;
using ApartmentsApp.Services.UserServices;
using ApartmentsApp.WebUI.Helpers;
using ApartmentsApp.WebUI.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using TalhaMarket.Service.MailSender;
using ApartmentsApp.API.Services;
using ApartmentsApp.API.Models;
using Microsoft.Extensions.Options;

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
            //jwt tokenı clientın okuması için
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //x.RequireHttpsMetadata = false;
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                  {
                      context.Token = context.Request.Cookies["jwt"];
                      return Task.CompletedTask;
                  },
                };
            });

            //mapping olusturma
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            //mapper objesini inject etmek için interfaceden kendi classımızdan oluşturduğumuz objeyi map için create ediyoruz.
            IMapper mapper = _mappingProfile.CreateMapper();
            //mapperı inject ediyoruz.
            services.AddSingleton(mapper);

            //servislerdeki dependency injection tanımlamaları
            services.AddScoped<IBillService, BillManager>();
            services.AddScoped<ICustomBillService, CustomBillManager>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<JwtService>();

            //Api katmanındaki mongodb bağlantıları için gerekli işlemler
            //mongodb connecttion stringinin eklenmesi
            services.Configure<CreditCardDatabaseSettings>(
                Configuration.GetSection(nameof(CreditCardDatabaseSettings)));

            services.AddSingleton<ICreditCardDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CreditCardDatabaseSettings>>().Value);

            services.AddSingleton<CreditCardService>();


            services.AddScoped<IMailService, MailManager>();
            //hangfire kurulumu için gerekli veritabanı bağlantılarını yapıyorum. ayrı bir databasede de tutulabilir.
            services.AddHangfire(x => x.UseSqlServerStorage("Server=TALHAEKREM;Database=ApartmentsDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddHangfireServer();
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //identity icin
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //single page application. yani react icin kurulum kısmı
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            //hangfire arayüzü. /hangfire ile giriş yapabilirsiniz.
            //app.UseHangfireDashboard();
        }
    }
}
