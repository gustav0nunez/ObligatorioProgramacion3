using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers.Api
{
    [Route("api/vehiculos")]
    [ApiController]
    [Authorize] 
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculoService _vehiculoService;

        public VehiculosController( VehiculoService vehiculoService )
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet("disponibles")]
        [Authorize(AuthenticationSchemes =
 JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDisponibles()
        {
            
            var vehiculosActivos = await _vehiculoService.ObtenerVehiculosActivos();

          
            return Ok(vehiculosActivos);
        }
    }
}
