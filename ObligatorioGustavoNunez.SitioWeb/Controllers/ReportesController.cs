using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReportesController : Controller
    {
        private readonly ReservaService _reservaService;

        public ReportesController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // GET: Reportes/ReservasPorFecha
        public async Task<IActionResult> ReservasPorFecha(DateTime? fechaInicio, DateTime? fechaFin)
        {
            
            if (!fechaInicio.HasValue || !fechaFin.HasValue)
            {
                return View();
            }

            var reservas = await _reservaService.ObtenerReservasEntreFechas(fechaInicio.Value, fechaFin.Value);

            
            ViewBag.TotalRecaudado = reservas.Sum(r => (r.FechaFin - r.FechaInicio).Days * r.Vehiculo.PrecioDiario);

            ViewBag.FechaInicio = fechaInicio.Value.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin.Value.ToString("yyyy-MM-dd");

            return View(reservas);
        }

        // GET: Reportes/ResumenEstadistico
        public async Task<IActionResult> ResumenEstadistico()
        {
            var reservas = await _reservaService.ObtenerTodas();

            ViewBag.Activas = reservas.Count(r =>
                r.Estado == ObligatorioGustavoNunez.Dominio.Entities.EstadoReserva.Pendiente ||
                r.Estado == ObligatorioGustavoNunez.Dominio.Entities.EstadoReserva.Confirmada ||
                r.Estado == ObligatorioGustavoNunez.Dominio.Entities.EstadoReserva.EnCurso);

            ViewBag.Canceladas = reservas.Count(r =>
                r.Estado == ObligatorioGustavoNunez.Dominio.Entities.EstadoReserva.Cancelada);

            if (reservas.Any())
            {
                ViewBag.Promedio = reservas.Average(r => (r.FechaFin - r.FechaInicio).Days * r.Vehiculo.PrecioDiario);
            }
            else
            {
                ViewBag.Promedio = 0;
            }

            return View();
        }


    }
}
