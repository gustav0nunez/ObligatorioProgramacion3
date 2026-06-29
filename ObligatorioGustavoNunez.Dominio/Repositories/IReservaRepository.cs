using ObligatorioGustavoNunez.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Repositories
{
    public interface IReservaRepository
    {
        Task AgregarReserva(Reserva reserva);
        Task <IEnumerable<Reserva>> ObtenerTodas();

        Task<Reserva> ObtenerPorId(int id);

        Task ModificarReserva(Reserva reserva);

        Task EliminarReserva(int id);
    }
}
