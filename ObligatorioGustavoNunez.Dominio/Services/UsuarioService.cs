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

        public UsuarioService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<Usuario> Guardar(Usuario v) => await _usuarioRepo.Guardar(v);
        public async Task<Usuario> Modificar(Usuario v) => await _usuarioRepo.Modificar(v);
        public async Task<Usuario> Eliminar(int id) => await _usuarioRepo.Eliminar(id);
        public async Task<Usuario> ObtenerPorId(int id) => await _usuarioRepo.ObtenerPorId(id);
        public async Task<List<Usuario>> ObtenerTodos() => await _usuarioRepo.ObtenerTodos();
        public async Task<Usuario> ValidarLogin(string email, string contraseña) => await _usuarioRepo.ValidarLogin(email, contraseña);
        public async Task AgregarUsuario(Usuario usuario) => await _usuarioRepo.AgregarUsuario(usuario);
    }
}