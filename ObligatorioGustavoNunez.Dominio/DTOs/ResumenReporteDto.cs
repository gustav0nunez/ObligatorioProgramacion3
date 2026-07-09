namespace ObligatorioGustavoNunez.Dominio.DTOs
{
    public class ResumenReporteDto
    {
        public int ReservasActivas { get; set; }
        public int ReservasCanceladas { get; set; }
        public decimal MontoPromedio { get; set; }
    }
}
