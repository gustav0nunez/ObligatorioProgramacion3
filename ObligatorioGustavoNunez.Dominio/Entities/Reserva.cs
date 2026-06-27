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


        public void CambiarEstado(EstadoReserva nuevoEstado)
        {
            switch (Estado)
            {
                case EstadoReserva.Pendiente:
                    if (nuevoEstado == EstadoReserva.Confirmada || nuevoEstado == EstadoReserva.Cancelada)
                        Estado = nuevoEstado;
                    else
                        throw new DomainException($"No se puede pasar de {Estado} a {nuevoEstado}.");
                    break;

                case EstadoReserva.Confirmada:
                    if (nuevoEstado == EstadoReserva.Cancelada || nuevoEstado == EstadoReserva.EnCurso)
                        Estado = nuevoEstado;
                    else
                        throw new DomainException($"No se puede pasar de {Estado} a {nuevoEstado}.");
                    break;

                case EstadoReserva.EnCurso:
                    if (nuevoEstado == EstadoReserva.Completada)
                        Estado = nuevoEstado;
                    else
                        throw new DomainException($"Bajo ninguna circunstancia una reserva En Curso puede retornar a Pendiente u otro estado que no sea Completada.");
                    break;

                default:
                    throw new DomainException($"Estado actual {Estado} no permite cambios.");
            }
        }
    }
}
