
using Prueba_nexu.BSD.Interfaz;
using Prueba_nexu.DAO.Interfaz;
using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;
using System.Reflection.Metadata.Ecma335;

namespace Prueba_nexu.BSD.Implementacion
{
	public class ModelsBSD : IModelsBSD
	{
		private readonly ILogger<ModelsBSD> _logger;
		private readonly IModelsDAO _modelDAO;
		public ModelsBSD(ILogger<ModelsBSD> logger, IModelsDAO modelsDAO)
		{
			_logger = logger;
			_modelDAO = modelsDAO;
		}

		public async Task<ModelDTO> ActualizarModelo(int id_modelo, decimal average_price)
		{
			try
			{
				Model Modelo = await _modelDAO.ActualizarModelo(id_modelo, average_price);
				return new ModelDTO ()
				{
					Id_Modelo = Modelo.id,
					Nombre = Modelo.nombre,
					Marca = new BrandDTO
					{
						Id = Modelo.id_brand,
						Nombre = Modelo.id_brandNavigation.nombre
					},
					Precio = Modelo.average_price
				};
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error al agregar el modelo");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al actualizar el modelo");
				throw;
			}
		}

		public async Task<ModelDTO> AgregarModelo(ModelDTO Modelo)
		{
			try
			{
				var ModeloDB = await _modelDAO.AgregarModelo(new Model
				{
					id = Modelo.Id_Modelo,
					nombre = Modelo.Nombre,
					average_price = Modelo.Precio,
					id_brand = Modelo.Marca.Id,
					id_brandNavigation = new brand
					{
						id = Modelo.Marca.Id,
						nombre = Modelo.Marca.Nombre
					}
				});
				return new ModelDTO
				{
					Id_Modelo = ModeloDB.id,
					Nombre = ModeloDB.nombre,
					Precio = ModeloDB.average_price,
					Marca = new BrandDTO
					{
						Id = ModeloDB.id_brand,
						Nombre = ModeloDB.id_brandNavigation.nombre
					}
				};
			}
			catch(ArgumentException ex)
			{
				_logger.LogError(ex, "Error al agregar el modelo");
				throw;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al agregar el modelo");
				throw;
			}
		}

		public async Task<List<ModelDTO>> ObtenerModeloMarca(int id_marca)
		{
			try
			{
				var ModelosD = await _modelDAO.ObternerModeloPorMarca(id_marca);
				return ModelosD.Select(x => new ModelDTO
				{
					Id_Modelo = x.id,
					Nombre = x.nombre,
					Precio = x.average_price,
					Marca= new BrandDTO
					{
						Id = x.id_brand,
						Nombre = x.id_brandNavigation.nombre
					}
				}).ToList();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al consultar el modelo de la marca");
				throw;
			}
		}

		public async Task<List<ModelDTO>> ObtenerModeloPorPrecio(decimal? greater, decimal? lower)
		{
			try
			{
				var ModelosD = await _modelDAO.ObtenerModeloPorPrecio(greater,lower);
				return ModelosD.Select(x => new ModelDTO
				{
					Id_Modelo = x.id,
					Nombre = x.nombre,
					Precio = x.average_price,
					Marca = new BrandDTO
					{
						Id = x.id_brand,
						Nombre = x.id_brandNavigation.nombre
					}
				}).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al consultar el modelo de la marca");
				throw;
			}
		}
	}
}
