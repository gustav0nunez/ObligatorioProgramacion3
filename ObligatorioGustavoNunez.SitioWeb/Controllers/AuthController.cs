using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ObligatorioGustavoNunez.Dominio.Repositories;
using ObligatorioGustavoNunez.Dominio.Entities;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepo;

        public AuthController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string contraseña)
        {
            var usuario = await _usuarioRepo.ValidarLogin(email, contraseña);

            if (usuario == null)
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
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }
            usuario.Rol = "Cliente";
            await _usuarioRepo.AgregarUsuario(usuario);

            return RedirectToAction("Login");
        }
    }
}

