using Microsoft.EntityFrameworkCore;
using ObligatorioGustavoNunez.Dominio.Entities;

namespace ObligatorioGustavoNunez.Persistencia.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
