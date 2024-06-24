using Microsoft.EntityFrameworkCore;
using Prueba_nexu.DAO.Interfaz;
using Prueba_nexu.DAO.Modelo;

namespace Prueba_nexu.DAO.Implementacion
{
	public class BrandDAO : IBrandDAO
	{
		public ILogger<BrandDAO> _logger;
		public AppDbContext _context;
		public BrandDAO(AppDbContext context, ILogger<BrandDAO> logger)
		{
			_context = context;
			this._logger = logger;
		}

		public async Task<brand> AgregarMarca(brand Marca)
		{
			try
			{
				// Verificar si el nombre de la marca es único
				if (await _context.brands.AnyAsync(p => p.nombre == Marca.nombre))
				{
					throw new ArgumentException("La marca "+Marca.nombre+" ya existe en el sistema ");
				}

				_context.brands.Add(Marca);
				await _context.SaveChangesAsync();
				return Marca;
			}
			catch (ArgumentException ex)
			{
				throw;
			}
			catch (Exception EX) 
			{
				_logger.LogError(EX, "Error al agregar la marca");
				throw;
			}
		}

		public async Task<List<brand>> ObtenerTodasMarcas()
		{
			try 
			{ 
				return await _context.brands.ToListAsync();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al consultar todas las marcas");
				throw ;
			}
		}
	}
}
