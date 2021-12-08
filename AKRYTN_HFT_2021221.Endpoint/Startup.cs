using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Logic;
using AKRYTN_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        }

        // Configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
