using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class VehiculoController : Controller
    {

        private readonly VehiculoService _vehiculoService;
        private readonly ReservaService _reservaService; 


            public VehiculoController(VehiculoService vehiculoService, ReservaService reservaService)
        {
            _vehiculoService = vehiculoService;
            _reservaService = reservaService;
        }

        // GET: Vehiculos
        public async Task <IActionResult> Index()
        {
            var vehiculos = await _vehiculoService.ObtenerTodos();
            return View(vehiculos);
        }

        // Get: Vehiculos/Create 
        public IActionResult Create()
        {
            return View();
        }

        //POST: Vehiculos/Create
        [HttpPost]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                await _vehiculoService.AgregarVehiculo(vehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        // GET Vehiculos/Edit/

        [HttpGet]
         public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var vehiculo = await _vehiculoService.ObtenerPorId(id.Value);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) {
                await _vehiculoService.ModificarVehiculo(vehiculo);
                return RedirectToAction(nameof(Index));
            }

            return View(vehiculo);
                }


        //GET: Vehiculos/Delete/

    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();          
            }

            var vehiculo = await _vehiculoService.ObtenerPorId(id.Value);
            if (vehiculo == null)
            {
                return NotFound(); 
            }

            return View(vehiculo); 
        }


        //POST: Vehiculos/Delete/

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _vehiculoService.ObtenerPorId(id);
            if (vehiculo == null) return NotFound();

            var reservasDelVehiculo = await _reservaService.ObtenerTodas();
            bool tieneReservas = reservasDelVehiculo.Any(r => r.VehiculoId == id);

            if (tieneReservas)
            {
                vehiculo.Estado = EstadoVehiculo.Inactivo; 
                await _vehiculoService.ModificarVehiculo(vehiculo);
                TempData["Exito"] = "El vehículo tiene reservas asociadas, por lo que fue marcado como Inactivo.";
            }
            else
            {
                await _vehiculoService.EliminarVehiculo(id);
                TempData["Exito"] = "Vehículo eliminado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
