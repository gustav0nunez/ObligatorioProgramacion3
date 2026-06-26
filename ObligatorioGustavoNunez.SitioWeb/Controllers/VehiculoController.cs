using Microsoft.AspNetCore.Mvc;
using ObligatorioGustavoNunez.Dominio;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using System.Runtime.InteropServices;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    public class VehiculoController : Controller
    {

        private readonly IVehiculoRepository _vehiculoRepo;


            public VehiculoController(IVehiculoRepository vehiculoRepo)
        {
            _vehiculoRepo = vehiculoRepo;
        }

        // GET: Vehiculos
        public async Task <IActionResult> Index()
        {
            var vehiculos = await _vehiculoRepo.ObtenerTodos();
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
                await _vehiculoRepo.Guardar(vehiculo);
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

            var vehiculo = await _vehiculoRepo.ObtenerPorId(id.Value);
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
                await _vehiculoRepo.Modificar(vehiculo);
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

            var vehiculo = await _vehiculoRepo.ObtenerPorId(id.Value);
            if (vehiculo == null)
            {
                return NotFound(); 
            }

            return View(vehiculo); 
        }


        //POST: Vehiculos/Delete/

        [HttpPost, ActionName("Delete")]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            await _vehiculoRepo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
