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
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository repository)
        {
            usuarioRepository = repository;
        }

        public async Task<Vehiculo> AgregarUsuario(Usuario usuario)
        {
            return await usuarioRepository.Guardar(usuario);
        }

        public async Task<Vehiculo> ModificarVehiculo(Usuario usuario)
        {
            return await usuarioRepository.Modificar(usuario);
        }

        public async Task<Usuario> EliminarUsuario(int id)
        {
            return await usuarioRepository.Eliminar(id);
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            return await usuarioRepository.ObtenerPorId(id);
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await usuarioRepository.ObtenerTodos();
        }
    }
}
