using AutoMapper;
using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business;
using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddSingleton<ProductInfoContext>();
            services.AddScoped<IQueryAll<Product>, GetAllProducts>();
            services.AddScoped<IQueryFor<string, Result<Product>>, GetProductById>();
            services.AddScoped<IQueryFor<ProductFilter,Result<IEnumerable<Product>>>, GetProductsByFilter>();
            services.AddScoped<ICommand<ProductForUpdate>,UpdateProduct>();
            services.AddScoped<ICommand<Product>, AddProduct>();
            services.AddScoped<ICommand<string>,DeleteProduct>();
            services.AddScoped<IQueryHandler, QueryHandler>();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title ="JB Hi Fi Product Mangemet Web API",
                    Description = "This application is used to get products and add products"
                });
            });

            //enable cors
            services.AddCors();
            //enable Jwt authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

            });

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Product, ProductForCreationDto>();
                cfg.CreateMap<Product, ProductForUpdateDto>();
            });
            IContainer container = new Container();

            container.Configure(q =>
            {
                q.AddRegistry<ApiBusinessRegistry>();
                q.Populate(services);
                q.For<IMapper>().Use(configuration.CreateMapper());
            });

            return container.GetInstance<IServiceProvider>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IMapper auto)
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
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JB Hi Fi API V1");
            });

            //use cors
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //use Jwt Authentication
            app.UseAuthentication();
        }
    }
}
