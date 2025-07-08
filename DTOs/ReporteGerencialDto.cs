namespace SafyroPresupuestos.DTOs
{
    public class ReporteGerencialDto
    {
        public Guid ProyectoId { get; set; }
        public decimal AvanceFisico { get; set; }
        public decimal AvanceFinanciero { get; set; }
    }

}
