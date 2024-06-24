namespace Prueba_nexu.DTO
{
	public class NuevaMarcaDTO
	{
		public String name { get; set; }
	}
	public class NuevoModeloDTO
	{
		public String name { get; set; }
		public Decimal average_price { get; set; }
	}
	public class ActualizarModeloDTO
	{
		public Decimal average_price { get; set; }
	}
}
