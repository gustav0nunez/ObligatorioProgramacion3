using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
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
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Id", "Matricula");

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

            ModelState.Remove("Usuario");
            ModelState.Remove("Vehiculo");

            if (reserva.Observaciones == null) reserva.Observaciones = "";

            if (ModelState.IsValid)
            {
                await _reservaService.AgregarReserva(reserva);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = new SelectList(await _usuarioService.ObtenerTodos(), "Id", "Email");
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Id", "Matricula");

            return View(reserva);
        }

        // GET: Reserva/Edit/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _reservaService.ObtenerPorId(id.Value);
            if (reserva == null)
            {
                return NotFound();
            }

            ViewBag.Usuarios = new SelectList(await _usuarioService.ObtenerTodos(), "Id", "Email", reserva.UsuarioId);
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Id", "Matricula", reserva.VehiculoId);

            return View(reserva);
        }

        // POST: Reserva/Edit/
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                ModelState.AddModelError("", "La fecha de inicio debe ser menor a la fecha de fin.");
            }

            if (reserva.Observaciones == null) reserva.Observaciones = "";
            ModelState.Remove("Usuario");
            ModelState.Remove("Vehiculo");

            if (ModelState.IsValid)
            {
                await _reservaService.ModificarReserva(reserva);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = new SelectList(await _usuarioService.ObtenerTodos(), "Id", "Email", reserva.UsuarioId);
            ViewBag.Vehiculos = new SelectList(await _vehiculoService.ObtenerTodos(), "Id", "Matricula", reserva.VehiculoId);

            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, EstadoReserva nuevoEstado)
        {
            var reserva = await _reservaService.ObtenerPorId(id);
            if (reserva == null) return NotFound();

            try
            {
                reserva.CambiarEstado(nuevoEstado);
                await _reservaService.ModificarReserva(reserva);
                TempData["Exito"] = "Estado actualizado correctamente.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reserva/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reserva = await _reservaService.ObtenerPorId(id.Value);
            if (reserva == null) return NotFound();

            return View(reserva);
        }

        // POST: Reserva/Delete/
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservaService.EliminarReserva(id);
            TempData["Exito"] = "Reserva eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
