using Microsoft.EntityFrameworkCore;

namespace ExploreWords.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins("http://localhost:8080")
									.AllowAnyHeader()
									.AllowAnyMethod();
			}));
			builder.Services.AddControllers();

			var connectionString = builder.Configuration.GetConnectionString("Default");

			builder.Services.AddDbContext<ExploreWordsDbContext>(m =>
			{
				m.UseSqlServer(connectionString);
				m.EnableSensitiveDataLogging();
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCors();


			app.MapControllers();

			app.Run();
		}
	}
}