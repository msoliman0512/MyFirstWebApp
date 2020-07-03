using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.Modal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebApp.WebSite.Models;
using MyFirstWebApp.WebSite.Services;

namespace MyFirstWebApp.WebSite
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
            services.AddRazorPages();
            services.AddServerSideBlazor();   // Add blazor service
            services.AddTransient<JsonFileProductService>(); // add our Service
            services.AddControllers(); // add our controllers such as ProductsController
           // services.AddBlazoredModal();  // Blazored.Moadl lib
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

             app.UseEndpoints(endpoints =>
              {
                  endpoints.MapRazorPages();
                  endpoints.MapControllers(); // it would be /Products cause we routed it in the Products Controller
                  endpoints.MapBlazorHub(); // Will manage all of the communication
                 // endpoints.Map("/products", (context) =>
                 //{
                 //  var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                 //   var json = JsonSerializer.Serialize(products);
                 //    return context.Response.WriteAsync(json);
                 //});

              });
        }
    }

}
