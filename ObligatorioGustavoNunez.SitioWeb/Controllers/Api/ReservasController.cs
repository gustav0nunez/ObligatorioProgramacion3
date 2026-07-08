using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers.Api
{
    [Route("api/reservas")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController( ReservaService reservaService )
        {
            _reservaService = reservaService;
        }

        [HttpGet("cliente/{id}")]
        public async Task<IActionResult> GetReservasPorCliente(int id)
        {
            var reservas = await _reservaService.ObtenerPorClienteId(id);
            if (reservas == null || !reservas.Any()) return NotFound("No hay reservas.");
            return Ok(reservas);

        }
    }
}
