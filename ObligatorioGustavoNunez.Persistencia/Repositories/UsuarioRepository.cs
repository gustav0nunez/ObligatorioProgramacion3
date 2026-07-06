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

        public async Task<Usuario> Guardar(Usuario v)
        {
            dbContext.Usuarios.Add(v);
            await dbContext.SaveChangesAsync();
            return v;
        }

        public async Task<Usuario> Modificar(Usuario v)
        {
            dbContext.Usuarios.Update(v);
            await dbContext.SaveChangesAsync();
            return v;
        }

        public async Task<Usuario> Eliminar(int id)
        {
            var usuario = await ObtenerPorId(id);
            if (usuario != null)
            {
                dbContext.Usuarios.Remove(usuario);
                await dbContext.SaveChangesAsync();
            }
            return usuario;
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> ValidarLogin(string email, string contraseña)
        {
            return await dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Contrasena == contraseña);
        }

        public async Task AgregarUsuario(Usuario usuario)
        {
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();
        }
       

        public async Task<Usuario> ObtenerPorMail(string email)
        {
            return await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
