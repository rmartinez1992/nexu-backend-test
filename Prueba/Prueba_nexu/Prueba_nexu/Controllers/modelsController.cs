using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prueba_nexu.BSD.Interfaz;
using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;

namespace Prueba_nexu.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class modelsController : ControllerBase
	{
		private readonly IModelsBSD _modelsBSD;
		private readonly IBrandBSD _brandBSD;
		private readonly ILogger<modelsController> _logger;

		public modelsController(AppDbContext context, ILogger<modelsController> logger, IBrandBSD brandBSD, IModelsBSD modelsBSD)
		{
			_brandBSD = brandBSD;
			_logger = logger;
			_modelsBSD = modelsBSD;
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<ModelDTO>> ActualizarModelo([FromBody] ActualizarModeloDTO Modelo, Int32 id)
		{
			try
			{
				return Ok(await _modelsBSD.ActualizarModelo(id, Modelo.average_price));
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al agregar modelo");
				return StatusCode(500, "An internal server error occurred."); // Devuelve error 500
			}


		}
		[HttpGet]
		public async Task<ActionResult<ModelDTO>> ObtenerModelosPorPresio([FromQuery] decimal? greater, [FromQuery] decimal? lower)
		{
			try
			{
				return Ok(await _modelsBSD.ObtenerModeloPorPrecio(greater,lower));
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al agregar modelo");
				return StatusCode(500, "An internal server error occurred."); // Devuelve error 500
			}


		}
	}
}
