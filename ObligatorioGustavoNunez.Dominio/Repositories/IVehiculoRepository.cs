using ObligatorioGustavoNunez.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Repositories
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo> Guardar(Vehiculo v);
        Task<Vehiculo> Modificar(Vehiculo v);
        Task<Vehiculo> Eliminar(int id);
        Task<Vehiculo> ObtenerPorId(int id);
        Task<List<Vehiculo>> ObtenerTodos();

    }
}
