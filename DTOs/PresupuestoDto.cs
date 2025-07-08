namespace SafyroPresupuestos.DTOs
{
    public class PresupuestoDto
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public string Nombre { get; set; }
        public decimal CostoMateriales { get; set; }
        public decimal CostoManoObra { get; set; }
        public decimal CostoDepreciacion { get; set; }
        public decimal CostoOtros { get; set; }
        public decimal CostoIndirectos { get; set; }
        public int MonedaId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}