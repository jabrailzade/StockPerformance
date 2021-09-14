using Contracts.Settings;
using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;
using System;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using WebApi.ApiClients;
using WebApi.Data;
using WebApi.Data.Repositories;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        private PolygonApiSettings polygonApiSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            polygonApiSettings = Configuration.GetSection(nameof(PolygonApiSettings)).Get<PolygonApiSettings>();
            services.AddRefitClient<IStockApiClient>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(polygonApiSettings.BaseUrl);
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", polygonApiSettings.ApiKey);
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<DataContext>(options => options.UseSqlite(Configuration.GetConnectionString(nameof(DataContext))));
            services.AddScoped<IStockService, StockService>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
