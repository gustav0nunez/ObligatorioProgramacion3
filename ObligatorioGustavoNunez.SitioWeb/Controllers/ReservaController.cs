using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class ReservaController : Controller
    {
        private readonly ReservaService _reservaService;
        private readonly UsuarioService _usuarioService;
        private readonly VehiculoService _vehiculoService;

        public ReservaController(ReservaService reservaService, UsuarioService usuarioService, VehiculoService vehiculoService)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _vehiculoService = vehiculoService;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaService.ObtenerTodas();
            return View(reservas);
        }

        // GET: Reserva/Crear
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = new SelectList(await _usuarioService.ObtenerTodos(), "Id", "Email");
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Matricula", "Matricula");

            return View();
        }

        // POST: Reserva/Create
        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                ModelState.AddModelError("", "La fecha de inicio debe ser menor a la fecha de fin.");
            }

            if (ModelState.IsValid)
            {
                await _reservaService.AgregarReserva(reserva);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = new SelectList(await _usuarioService.ObtenerTodos(), "Id", "Email");
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Matricula", "Matricula");

            return View(reserva);
        }
    }
}
