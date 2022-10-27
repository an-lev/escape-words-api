using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExploreWords.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
                builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

            // Add services to the container.
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(builder.Configuration.GetSection("Origin").Get<string[]>())
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "PATCH", "DELETE")
                        .AllowCredentials();
            }));
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<ExploreWordsDbContext>(m =>
            {
                m.UseSqlServer(connectionString);
                m.EnableSensitiveDataLogging();
            });
            builder.Services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseDefaultFiles();

            app.UseSpaStaticFiles();

            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(_ => { });

            app.Run();
        }
    }
}