using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private List<Usuario> _usuariosMemoria;

        public UsuarioService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            _usuariosMemoria = new List<Usuario>()
            {
                new Usuario() { Email = "admin@ctcsalto.com", Contrasena = "admin123", Rol = "Administrador" },
                new Usuario() { Email = "operador@ctcsalto.com", Contrasena = "operador123", Rol = "Operador" }
            };
        }

        public async Task<Usuario> ObtenerUsuario(string email)
        {
            var usuarioMemoria = _usuariosMemoria.FirstOrDefault(u => u.Email == email);
            if (usuarioMemoria != null)
            {
                return usuarioMemoria;
            }

            return await _usuarioRepo.ObtenerPorMail(email);
        }

        public async Task<Usuario> Guardar(Usuario v) => await _usuarioRepo.Guardar(v);
        public async Task<Usuario> Modificar(Usuario v) => await _usuarioRepo.Modificar(v);
        public async Task<Usuario> Eliminar(int id) => await _usuarioRepo.Eliminar(id);
        public async Task<Usuario> ObtenerPorId(int id) => await _usuarioRepo.ObtenerPorId(id);
        public async Task<List<Usuario>> ObtenerTodos() => await _usuarioRepo.ObtenerTodos();
        public async Task<Usuario> ValidarLogin(string email, string contraseña) => await _usuarioRepo.ValidarLogin(email, contraseña);
        public async Task AgregarUsuario(Usuario usuario) => await _usuarioRepo.AgregarUsuario(usuario);

        public async Task<Usuario> ObtenerPorEmail(string email)
        {
            return await _usuarioRepo.ObtenerPorMail(email);
        }
    }
}