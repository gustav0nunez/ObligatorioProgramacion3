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
    public class VehiculoRepository : IVehiculoRepository
    {
        
        
            private readonly AppDbContext dbContext;

            public VehiculoRepository(AppDbContext context)
            {
                dbContext = context;
            }

            public async Task<Vehiculo> Eliminar(int id)
            {
                var vehiculo = await dbContext.Vehiculos.FindAsync(id);
                if (vehiculo != null)
                {
                    dbContext.Vehiculos.Remove(vehiculo);
                    await dbContext.SaveChangesAsync();
                }
                return vehiculo;
            }

            public async Task<Vehiculo> Guardar(Vehiculo v)
            {
                dbContext.Vehiculos.Add(v);
                await dbContext.SaveChangesAsync();
                return v;
            }

            public async Task<Vehiculo> Modificar(Vehiculo v)
            {
                dbContext.Entry(v).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return v;
            }

            public async Task<Vehiculo> ObtenerPorId(int id)
            {
            return await dbContext.Vehiculos.FindAsync(id);
                  
            }

        public async Task<List<Vehiculo>> ObtenerTodos()
        {
            return await dbContext.Vehiculos.ToListAsync();
        }
    }
}
