using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObligatorioGustavoNunez.SitioWeb.Controllers.Api
{
    [Route("api/auth")] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioService _usuarioService; 

        public AuthController(IConfiguration configuration, UsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginApi([FromBody] LoginDto login)
        {
            var usuEncontrado = await _usuarioService.ValidarLogin(login.Email, login.Password);

            if (usuEncontrado != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuEncontrado.Email),
            new Claim(ClaimTypes.Role, usuEncontrado.Rol)
        };

                var token = GenerateJwtToken(claims);

                return Ok(new { token = token });
            }

            return Unauthorized(new { error = "Credenciales inválidas" });
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}