using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Endpoint.Services;
using AKRYTN_HFT_2021221.Logic;
using AKRYTN_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221_Endpoint
{
    public class Startup
    {
        // Add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Need this because of the circular reference(source: https://gavilan.blog/2021/05/19/fixing-the-error-a-possible-object-cycle-was-detected-in-different-versions-of-asp-net-core/)
            services.AddControllers().AddJsonOptions(x =>
             x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //Add controllers:
            //services.AddControllers();
            //Publisher
            services.AddTransient<IPublisherLogic, PublisherLogic>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            //Book
            services.AddTransient<IBookLogic, BookLogic>();
            services.AddTransient<IBookRepository, BookRepository>();
            //CartItem
            services.AddTransient<ICartItemLogic, CartItemLogic>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            //Cart
            services.AddTransient<ICartLogic, CartLogic>();
            services.AddTransient<ICartRepository, CartRepository>();
            //User
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IUserRepository, UserRepository>();
            //DbContext
            services.AddScoped<BookStoreDbContext, BookStoreDbContext>();
            //SignalR
            services.AddSignalR();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Endpoint", Version = "v1" });
            });
        }

        // Configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));


            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:11702"));


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
