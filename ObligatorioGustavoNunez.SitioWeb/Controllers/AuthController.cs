using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ObligatorioGustavoNunez.Dominio.Repositories;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Services;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    public class AuthController : Controller
    {

        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioServi)
        {
            _usuarioService = usuarioServi;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string contrasena)
        {
            var usuario = await _usuarioService.ObtenerUsuario(email);

            if (usuario == null || usuario.Contrasena != contrasena)
            {
                ViewBag.Error = "Usuario y/o contraseña incorrecta";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.Email),
        new Claim(ClaimTypes.Role, usuario.Rol)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        //GET: Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        //GET: Auth/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        //POST: Auth/Registrar
        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            usuario.Rol = "Cliente";
            ModelState.Remove("Rol");

            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var usuarioExistente = await _usuarioService.ObtenerPorEmail(usuario.Email);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "Este correo ya está registrado.");
                return View(usuario);
            }

            await _usuarioService.AgregarUsuario(usuario);

            return RedirectToAction("Login");
        }
    }
}

