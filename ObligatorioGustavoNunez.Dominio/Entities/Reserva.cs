using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Entities
{
    public enum EstadoReserva
    {
        Pendiente,
        Confirmada,
        EnCurso,
        Completada,
        Cancelada
    }

    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadDePersonas { get; set; }
        public EstadoReserva Estado { get; set; } = EstadoReserva.Pendiente;
        public string Observaciones { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string VehiculoMatricula { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
