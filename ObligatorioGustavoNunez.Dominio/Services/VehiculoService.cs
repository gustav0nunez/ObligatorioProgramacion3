using Microsoft.EntityFrameworkCore;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Services
{
    public class VehiculoService
    {
        private readonly IVehiculoRepository vehiculoRepository;

        public VehiculoService(IVehiculoRepository vehiculoRepo)
        {
            vehiculoRepository = vehiculoRepo;
        }

        public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
        {
            return await vehiculoRepository.Guardar(vehiculo);
        }

        public async Task<Vehiculo> ModificarVehiculo(Vehiculo vehiculo)
        {
            return await vehiculoRepository.Modificar(vehiculo);
        }

        public async Task<Vehiculo> EliminarVehiculo(int id)
        {
            return await vehiculoRepository.Eliminar(id);
        }

        public async Task<Vehiculo> ObtenerPorId(int id)
        {
            return await vehiculoRepository.ObtenerPorId(id);
        }

        public async Task<List<Vehiculo>> ObtenerTodos()
        {
            return await vehiculoRepository.ObtenerTodos();
        }

        public async Task<List<Vehiculo>> ObtenerVehiculosActivos()
        {
           
            var todosLosVehiculos = await vehiculoRepository.ObtenerTodos();

            return todosLosVehiculos.Where(v => v.Estado == EstadoVehiculo.Activo).ToList();
        }
    }
}

