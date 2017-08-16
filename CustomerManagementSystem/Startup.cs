using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem
{
    public class Startup
    {
        //uygulama ilk çalıştığında buası çalışır buraya ayarlarıızı koyarız
        public Startup(IHostingEnvironment env)
        {
            //yapıcı metod
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //appsettings json dosyasında bir ayar yaptık ama bu ayarı 25.satır olanda ezebiliriz
                //son yazılan ayarlar ilk ayarları ezer.
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            //configure servisinin amacı dependency injection katmanını hazırlamaktır
            //dependency önemlibir kavramdır .core da dependencyi kullanmak zorundayız
            //dependency injection frameworklerinden istediğimizi kullanabiliriz
            //services değişkeni .core dependency tarafından yaratılan bir nesne olup tüm dependencylerimizi buna ekleriz
            // Add framework services.
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlServer(sqlConnectionString);
            });

            //repositorylerimizi buraya servislere eklicez.
            services.AddScoped<ICustomersRepository, CustomerRepository>();
            //ıcustomers repository, customers repository
            //ıcustomers repository referansı gördüğün her yerde (constructorda) karşılığında customersrepositorynesnesi yarat ve enjecte et diyor.
            //addscoped: request boyunca bu nesne hayatta kalır. aynı nesneyi response gidene kadar contanerutar. controllera istek gelince customer rep. nesnesi yaratılır ve response gönderildiğide o nesne kaybolur. her requeste bu nesne yenilemiş olur
            services.AddScoped<IStatesRepository, StatesRepository>();

            services.AddTransient<DbSeeder>();
            //transient: nesneyi yaratıp kaydedince nesnenin işi biter bitmez nesne yok edilir. 
            services.AddMvc();
            //bu srvis uygulamayı mvcye göre ayarla demektir.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbSeeder dbSeeder)
        {
            //bu metodlda requestlerin nasıl karşılanacağına dair yazılan kodlardır.
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loglamanın nasıl yapılacağını bildirir
            loggerFactory.AddDebug();
            //hem console hem de debug klasörüneloglama yapılması gerektiği yazılmış
            dbSeeder.SeedAsync().Wait();
            app.UseMvc();

            //yüklenmiş olan paketler .csproj dosyasına kaydedilir odosyaya ise projeye sağ tık yapıp edi derseniz gidebilirsiniz
        }
    }
}
