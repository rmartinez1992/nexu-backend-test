
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Prueba_nexu.BSD.Implementacion;
using Prueba_nexu.BSD.Interfaz;
using Prueba_nexu.DAO.Implementacion;
using Prueba_nexu.DAO.Interfaz;
using Prueba_nexu.DAO.Modelo;
using System;

namespace Prueba_nexu
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			var configuration = new ConfigurationBuilder()
		   .SetBasePath(AppContext.BaseDirectory)
		   .AddJsonFile("appsettings.json")
			.Build();

			builder.Services.AddDbContext<AppDbContext>(options =>
			   options.UseSqlServer(configuration.GetConnectionString("DB_Prueba")));
			//Inyeccion de dependencias
			builder.Services.AddScoped<IBrandDAO, BrandDAO>();
			builder.Services.AddScoped<IBrandBSD, BrandBSD>();
			builder.Services.AddScoped<IModelsDAO, ModelsDAO>();
			builder.Services.AddScoped<IModelsBSD, ModelsBSD>();

			// Configurar logging
			builder.Host.ConfigureLogging(logging =>
			{
				logging.ClearProviders(); // Limpiar los proveedores de logging existentes
				logging.AddConsole();     // Agregar el proveedor de logging para la consola
			});




			var app = builder.Build();

			// Configure the HTTP request pipeline.
			
				app.UseSwagger();
				app.UseSwaggerUI();
			

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}

	}
}
