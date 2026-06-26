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
    public class UsuarioRepository : IUsuarioRepository
    {


        private readonly AppDbContext dbContext;

        public UsuarioRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public Task<Usuario> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Guardar(Usuario v)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Modificar(Usuario v)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ValidarLogin(string email, string contraseña)
        {
            return await dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == contraseña);
        }

        public async Task AgregarUsuario(Usuario usuario)
        {
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();
        }
    }
}
