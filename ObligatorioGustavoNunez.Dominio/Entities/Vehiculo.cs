using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ObligatorioGustavoNunez.Dominio.Entities
{
    public enum EstadoVehiculo
    {
        Activo,
        Inactivo
    }

    [Index(nameof(Matricula), IsUnique = true)]
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Matricula { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        public int CapacidadPasajeros { get; set; }
        public float PrecioDiario { get; set; }
        public EstadoVehiculo Estado { get; set; } = EstadoVehiculo.Activo;

    }
}
