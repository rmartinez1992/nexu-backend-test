
using Microsoft.EntityFrameworkCore;
using Prueba_nexu.BSD.Implementacion;
using Prueba_nexu.DAO.Interfaz;
using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;
using System.Text.RegularExpressions;

namespace Prueba_nexu.DAO.Implementacion
{
	public class ModelsDAO : IModelsDAO
	{
		private readonly ILogger<ModelsDAO> _logger;
		private readonly AppDbContext _context;

		public ModelsDAO (ILogger<ModelsDAO> logger, AppDbContext _context)
		{
			_logger = logger;
			this._context = _context;
		}

		public async Task<Model> ActualizarModelo(Int32 id_modelo, decimal average_price)
		{
			try
			{
				Model Modelo = await _context.Models.Include(p=>p.id_brandNavigation).FirstOrDefaultAsync(p => p.id == id_modelo);
				// Validamos que la marca exista en el sistem
				if (Modelo == null)
				{
					throw new ArgumentException("El modelo no existe en el sistema ");
				}
				if (average_price > 0)
				{
					if (average_price < 100000)
					{
						throw new ArgumentException("El precion debe ser superior a 100.000");
					}
				}
				Modelo.average_price = average_price;
				await _context.SaveChangesAsync();
				return Modelo;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al actualizar el modelo");
				throw;
			}
		}

		public async Task<Model> AgregarModelo(Model Modelo)
		{
			try
			{
				brand Marca= await _context.brands.FirstOrDefaultAsync(p => p.id == Modelo.id_brandNavigation.id);
				// Validamos que la marca exista en el sistem
				if (Marca==null)
				{
					throw new ArgumentException("La marca " + Modelo.id_brandNavigation.nombre + " no existe en el sistema ");
				}
				Modelo.id_brandNavigation=Marca;
				if (await _context.Models.AnyAsync(p => p.nombre == Modelo.nombre && p.id_brand==Marca.id))
				{
					throw new ArgumentException("El modelo  " + Modelo.nombre + " de la marca "+Modelo.id_brandNavigation.nombre +" ya existe en el sistema ");
				}
				if(Modelo.average_price<0)
				{
					throw new ArgumentException("El precio del modelo no puede ser negativo");
				}
				if(Modelo.average_price > 0)
				{
					if(Modelo.average_price < 100000)
					{
						throw new ArgumentException("El precion debe ser superior a 100.000");
					}
				}
				_context.Models.Add(Modelo);
				await _context.SaveChangesAsync();
				return Modelo;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al agregar el modelo");
				throw;
			}
		}

		public async Task<List<Model>> ObtenerModeloPorPrecio(decimal? greater, decimal? lower)
		{
			try
			{
				var models = new List<Model>();

				if (greater.HasValue)
				{
					models.AddRange( _context.Models.Include(p => p.id_brandNavigation).Where(m => m.average_price > greater.Value && m.average_price != 0));
				}

				if (lower.HasValue)
				{
					models.AddRange(_context.Models.Include(p => p.id_brandNavigation).Where(m => m.average_price < lower.Value && m.average_price!=0 
					&& !(from x in models select x.id).ToList().Contains(m.id)));
				}

				return models;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al consultar los modelos por marca");
				throw;
			}
		}

		public async Task<decimal> ObtenerPrecioPromedi(int Id_Marca)
		{
			try
			{

				// Obtiene los precios de la base de datos
				var precios = await _context.Models
					.Where(ml => ml.id_brand == Id_Marca)
					.Select(ml => (decimal)ml.average_price) // Usar decimal? para manejar nulos
					.ToListAsync();

				// Calcula el promedio en el lado del cliente, devolviendo 0 si no hay elementos
				var averagePrice = precios.DefaultIfEmpty(0).Average();

				return averagePrice;
			
				

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al consultar el precio promedio de los modelos");
				throw;
			}

		}

		public Task<List<Model>> ObternerModeloPorMarca(int Id_Marca)
		{
			try
			{
				return _context.Models
					.Where(m => m.id_brand == Id_Marca)
					.Include(m=> m.id_brandNavigation)
					.ToListAsync();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al consultar los modelos por marca");
				throw;
			}
		}
	}
}
