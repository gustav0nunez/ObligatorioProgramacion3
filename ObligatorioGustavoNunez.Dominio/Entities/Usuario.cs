using System.ComponentModel.DataAnnotations;

namespace ObligatorioGustavoNunez.Dominio.Entities
    
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Documento { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contraseña { get; set; }
        public string Pais { get; set; }
        public string Sexo { get; set; }
        public string Rol { get; set; } = "Cliente";

    }
}
