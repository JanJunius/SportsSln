using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        private IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Initialisiert Services für das MVC-Framework und die Razor-Engine
            services.AddControllersWithViews();

            // Initialisert Entity Framework Core
            services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:SportsStoreConnection"]);
            });

            // Inversion of Control
            // Die Services werden nie direkt angesprochen, sondern immer über die zugehörigen Interfaces.
            // Die Zuordnung wer was implementiert erfolgt hier.
            // Eine Komponente wie z.B. ein PageModel definiert einen Konstruktor mit dem Interfacetyp, den sie benötigt
            // und erhält diesen dann abhängig von dieser Konfiguration geliefert.
            // Auf diese Weise kann man hier eine Konfiguration vollständig ersetzen, indem man z.B. als implementierende
            // Klasse eine MOC-Version zum Testen angibt.
            services.AddScoped<IStoreRepository, EFStoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                {
                    // Optional zum "besseren" Aussehen der Links
                    // Ohne: http://localhost:5000/?productPage=2
                    // Mit: http://localhost:5000/Products/Page2
                    endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "index" } );

                    // Registriert das MVC-Framework für das EndPoint-Routing
                    endpoints.MapDefaultControllerRoute(); 
                }
            });

            // Initiales Füllen der DB mit Testdaten falls erforderlich
            SeedData.EnsurePopulated(app);
        }
    }
}
