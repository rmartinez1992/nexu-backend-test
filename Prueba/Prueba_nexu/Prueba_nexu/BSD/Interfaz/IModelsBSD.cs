using Prueba_nexu.DTO;

namespace Prueba_nexu.BSD.Interfaz
{
	public interface IModelsBSD
	{
		public Task<List<ModelDTO>>ObtenerModeloMarca(Int32 id_marca);
		public Task<ModelDTO> AgregarModelo(ModelDTO Modelo);
		public Task<ModelDTO> ActualizarModelo(Int32 id_modelo, decimal average_price);
		public Task<List<ModelDTO>> ObtenerModeloPorPrecio(decimal? greater, decimal? lower);
	}
}
