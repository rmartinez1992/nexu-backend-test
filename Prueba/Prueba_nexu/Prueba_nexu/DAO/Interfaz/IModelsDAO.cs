using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;

namespace Prueba_nexu.DAO.Interfaz
{
	public interface IModelsDAO
	{
		public Task<Decimal> ObtenerPrecioPromedi(Int32 Id_Marca);

		public Task<List<Model>> ObternerModeloPorMarca(Int32 Id_Marca);
		public Task<Model> AgregarModelo(Model Modelo);
		public Task<Model> ActualizarModelo(Int32 id_modelo, decimal average_price);
		public Task<List<Model>> ObtenerModeloPorPrecio(decimal? greater, decimal? lower);
	}
}
