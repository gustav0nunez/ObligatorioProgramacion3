using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio.DTOs;
using ObligatorioGustavoNunez.Dominio.Entities;
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

        [HttpPatch("estado")]
        public async Task<IActionResult> ActualizarEstado([FromBody] ActualizarEstadoReservaDto dto)
        {
            try
            {
                await _reservaService.ActualizarEstado(dto);

                return Ok(new { exitoso = true, mensaje = "Estado actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] CrearReservaDto dto)
        {
            try
            {
                var nuevaReserva = await _reservaService.CrearReserva(dto);

                return CreatedAtAction(nameof(GetReservasPorCliente), new { id = nuevaReserva.UsuarioId }, nuevaReserva);
            }
            catch (DomainException ex) when (ex.Message.Contains("disponible"))
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("/api/reportes/resumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            try
            {
                var resumen = await _reservaService.ObtenerResumen();
                return Ok(resumen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}
