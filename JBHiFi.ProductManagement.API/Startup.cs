using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business;
using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;

namespace JBHiFi.ProductManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<ProductInfoContext>();
            services.AddScoped<IQueryAll<Product>, GetAllProducts>();
            services.AddScoped<IQueryFor<string, Result<Product>>, GetProductById>();
            services.AddScoped<IQueryFor<ProductFilter,Result<IEnumerable<Product>>>, GetProductsByFilter>();
            services.AddScoped<ICommand<ProductForUpdate>,UpdateProduct>();
            services.AddScoped<ICommand<Product>, AddProduct>();
            services.AddScoped<ICommand<string>,DeleteProduct>();
            services.AddScoped<IQueryHandler, QueryHandler>();
            IContainer container = new Container();

            container.Configure(q =>
            {
              
                q.AddRegistry<ApiBusinessRegistry>();
                
                q.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStatusCodePages();


        }
    }
}
