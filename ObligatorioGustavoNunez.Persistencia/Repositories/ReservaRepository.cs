using Microsoft.EntityFrameworkCore;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using ObligatorioGustavoNunez.Persistencia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Persistencia.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly AppDbContext dbContext;

        public ReservaRepository(AppDbContext context) { 
        dbContext = context;
        
        }

        public async Task AgregarReserva(Reserva reserva)
        {
            dbContext.Reservas.Add(reserva);
            await dbContext.SaveChangesAsync();
        }

        public async Task ModificarReserva(Reserva reserva)
        {
            dbContext.Reservas.Update(reserva);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Reserva> ObtenerPorId(int id)
        {
            return await dbContext.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Vehiculo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reserva>> ObtenerTodas()
        {
            return await dbContext.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Vehiculo)
                .ToListAsync();
        }

        public async Task EliminarReserva(int id)
        {
            var reserva = await dbContext.Reservas.FindAsync(id);
            if (reserva != null)
            {
                dbContext.Reservas.Remove(reserva);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}


