using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_nexu.BSD.Interfaz;
using Prueba_nexu.DAO.Modelo;
using Prueba_nexu.DTO;

namespace Prueba_nexu.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly IModelsBSD _modelsBSD;
		private readonly IBrandBSD _brandBSD;	
		private readonly ILogger<BrandsController> _logger;

		public BrandsController(AppDbContext context, ILogger<BrandsController> logger,IBrandBSD brandBSD, IModelsBSD modelsBSD)
		{
			_brandBSD = brandBSD;
			_logger = logger;
			_modelsBSD = modelsBSD;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
		{
			try
			{
				return Ok( await _brandBSD.ObtenerMarcar());
			}
			catch
			{
				return StatusCode(500, "An internal server error occurred."); // Devuelve error 500
			}
		}
		[HttpGet("{id}/models")]
		public async Task<ActionResult<IEnumerable<ModelDTO>>> ObtenerModelos(int id)
		{
			try
			{
				var models = await _modelsBSD.ObtenerModeloMarca(id);
				if (models.Count == 0)
				{
					return NotFound();
				}

				return Ok(models);
			}
			catch( Exception ex)
			{

				return StatusCode(500, "An internal server error occurred."); // Devuelve error 500
			}
		}
		[HttpPost]
		public async Task<ActionResult<BrandDTO>> AgregarrMarca([FromBody] NuevaMarcaDTO Marca)
		{
			try
			{
				return Ok(await _brandBSD.AgregarMarca(new BrandDTO { Nombre=Marca.name}));
			}
			catch( ArgumentException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error al agregar marca");
				return StatusCode(500, "An internal server error occurred."); // Devuelve error 500
			}

			
		}
		
		[HttpPost("{id}/models")]
		public async Task<ActionResult<ModelDTO>> AddModelo([FromBody] NuevoModeloDTO Modelo, Int32 id)
		{
			try
			{
				return Ok(await _modelsBSD.AgregarModelo(new ModelDTO
				{
					Marca = new BrandDTO { Id = id },
					Nombre = Modelo.name,
					Precio = Modelo.average_price	
				}));
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
