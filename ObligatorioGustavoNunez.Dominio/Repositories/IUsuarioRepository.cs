using ObligatorioGustavoNunez.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ValidarLogin(string email, string contraseña);

        Task AgregarUsuario(Usuario usuario);
        Task<Usuario> Guardar(Usuario v);
        Task<Usuario> Modificar(Usuario v);
        Task<Usuario> Eliminar(int id);
        Task<Usuario> ObtenerPorId(int id);
        Task<List<Usuario>> ObtenerTodos();

        Task<Usuario> ObtenerPorMail(string email);

    }
}
