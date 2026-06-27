using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _reservaRepo;

        public ReservaService(IReservaRepository reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        public async Task AgregarReserva(Reserva reserva)
        {
            await _reservaRepo.AgregarReserva(reserva);
        }

        public async Task<IEnumerable<Reserva>> ObtenerTodas()
        {
            return await _reservaRepo.ObtenerTodas();
        }
    }
}
