using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioGustavoNunez.Dominio.Entities
{

    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public string Sexo { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; 

        [Required]
        public string Contrasena { get; set; } = string.Empty;

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public string Pais { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty; 
    }
}