using Prueba_nexu.BSD.Interfaz;
using Prueba_nexu.DAO.Interfaz;
using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;

namespace Prueba_nexu.BSD.Implementacion
{
	public class BrandBSD : IBrandBSD
	{
		ILogger<BrandBSD> _logger;
		IBrandDAO _brandDAO;
		IModelsDAO _modelsDAO;
		public BrandBSD(IBrandDAO brandDAO, ILogger<BrandBSD> logger,IModelsDAO modelsDAO)
		{
			_brandDAO = brandDAO;
			this._logger = logger;
			_modelsDAO = modelsDAO;
		}

		public async Task<BrandDTO> AgregarMarca(BrandDTO Marca)
		{
			try
			{
				var MarcaDB = await _brandDAO.AgregarMarca(new brand
				{
					nombre = Marca.Nombre,
				});	
				return  (new BrandDTO
				{
					Id = MarcaDB.id,
					Nombre = MarcaDB.nombre
				});
			}
			catch(ArgumentException ex)
			{
				throw;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al agregar marca BSD");
				throw;
			}
		}

		public async Task<List<BrandDTO>> ObtenerMarcar()
		{
			try
			{
				List<brand> MarcasDB = await _brandDAO.ObtenerTodasMarcas();
				return MarcasDB.Select(x => new BrandDTO
				{
					Id = x.id,
					Nombre = x.nombre,
					Precio_Promedio= _modelsDAO.ObtenerPrecioPromedi(x.id).Result
				}).ToList();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al consultar todas las marcas BSD");
				throw;
			}
		}
	}
}
