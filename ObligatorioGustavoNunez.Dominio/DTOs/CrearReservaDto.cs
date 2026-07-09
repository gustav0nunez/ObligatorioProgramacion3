

namespace ObligatorioGustavoNunez.Dominio.DTOs
{
    public  class CrearReservaDto
    {
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadPasajeros { get; set; }
    }
}
