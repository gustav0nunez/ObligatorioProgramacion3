using ObligatorioGustavoNunez.Dominio.Entities;
using ObligatorioGustavoNunez.Dominio.Repositories;
using ObligatorioGustavoNunez.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IVehiculoRepository _vehiculoRepo;

        public ReservaService(IReservaRepository reservaRepo, IVehiculoRepository vehiculoRepo  )
        {
            _reservaRepo = reservaRepo;
            _vehiculoRepo = vehiculoRepo;
        }

        public async Task AgregarReserva(Reserva reserva)
        {
            await _reservaRepo.AgregarReserva(reserva);
        }

        public async Task<IEnumerable<Reserva>> ObtenerTodas()
        {
            return await _reservaRepo.ObtenerTodas();
        }

        public async Task<Reserva> ObtenerPorId(int id)
        {
            return await _reservaRepo.ObtenerPorId(id);
        }

        public async Task ModificarReserva(Reserva reserva)
        {
             await _reservaRepo.ModificarReserva(reserva);
        }

        public async Task EliminarReserva(int id)
        {
            await _reservaRepo.EliminarReserva(id);
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var todasLasReservas = await ObtenerTodas();

            return todasLasReservas
                .Where(r => r.FechaInicio >= fechaInicio && r.FechaFin <= fechaFin)
                .ToList();
        }

        public async Task<List<Reserva>> ObtenerPorClienteId(int clienteId)
        {
            
            var todasLasReservas = await _reservaRepo.ObtenerTodas();

            return todasLasReservas.Where(r => r.UsuarioId == clienteId).ToList();
        }


        public async Task ActualizarEstado(ActualizarEstadoReservaDto dto)
        {
            var reserva = await _reservaRepo.ObtenerPorId(dto.Id);
            if (reserva == null) throw new Exception("Reserva no encontrada");

            if (Enum.TryParse<EstadoReserva>(dto.Estado, true, out var nuevoEstado))
            {
                reserva.CambiarEstado(nuevoEstado); 
                reserva.Observaciones = dto.Observaciones;

                await _reservaRepo.ModificarReserva(reserva);
            }
            else
            {
                throw new Exception("El estado ingresado no existe.");
            }
        }

        public async Task<Reserva> CrearReserva(CrearReservaDto dto)
        {
           
            if (dto.FechaFin <= dto.FechaInicio)
                throw new DomainException("La fecha de fin debe ser mayor a la fecha de inicio.");

            
            var vehiculo = await _vehiculoRepo.ObtenerPorId(dto.VehiculoId);
            if (vehiculo == null)
                throw new Exception("Vehículo no encontrado.");

            if (vehiculo.CapacidadPasajeros < dto.CantidadPasajeros)
                throw new DomainException("La capacidad del vehículo no alcanza para la cantidad de pasajeros solicitada.");

          
            var todasLasReservas = await _reservaRepo.ObtenerTodas();
            bool estaOcupado = todasLasReservas.Any(r =>
                r.VehiculoId == dto.VehiculoId &&
                r.Estado != EstadoReserva.Cancelada &&
                r.FechaInicio < dto.FechaFin &&
                r.FechaFin > dto.FechaInicio);

            if (estaOcupado)
                throw new DomainException("El vehículo no está disponible en esas fechas.");

            
            var nuevaReserva = new Reserva
            {
                VehiculoId = dto.VehiculoId,
                UsuarioId = dto.ClienteId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                CantidadDePersonas = dto.CantidadPasajeros,
                Estado = EstadoReserva.Pendiente, 
                Observaciones = ""
            };

             await _reservaRepo.AgregarReserva(nuevaReserva);
            return nuevaReserva;
        }

        public async Task<ResumenReporteDto> ObtenerResumen()
        {
            var todasLasReservas = await _reservaRepo.ObtenerTodas(); 

            var reservasActivas = todasLasReservas.Count(r =>
                r.Estado == EstadoReserva.Pendiente ||
                r.Estado == EstadoReserva.Confirmada ||
                r.Estado == EstadoReserva.EnCurso);

            var reservasCanceladas = todasLasReservas.Count(r => r.Estado == EstadoReserva.Cancelada);

            
            var montoPromedio = todasLasReservas.Any()
                ? todasLasReservas.Average(r => (r.FechaFin - r.FechaInicio).Days * r.Vehiculo.PrecioDiario)
                : 0;

            return new ResumenReporteDto
            {
                ReservasActivas = reservasActivas,
                ReservasCanceladas = reservasCanceladas,
                MontoPromedio = (decimal)montoPromedio
            };
        }
    }
}
