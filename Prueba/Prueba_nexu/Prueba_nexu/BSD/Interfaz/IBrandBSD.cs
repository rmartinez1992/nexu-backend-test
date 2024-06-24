using Prueba_nexu.DTO;

namespace Prueba_nexu.BSD.Interfaz
{
	public interface IBrandBSD
	{
		public Task<List<BrandDTO>> ObtenerMarcar();
		public Task<BrandDTO> AgregarMarca(BrandDTO Marca);
	}
}
