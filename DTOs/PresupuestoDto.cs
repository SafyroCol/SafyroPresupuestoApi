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
        public string? MonedaNombre { get; set; }
        public string? MonedaCodigo { get; set; }
        public string? MonedaSimbolo { get; set; }
        public int MonedaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        // NUEVO: Costos Reales (puede ser un objeto, o props sueltas)
        public decimal? CostoRealMateriales { get; set; }
        public decimal? CostoRealManoObra { get; set; }
        public decimal? CostoRealEquipos { get; set; }
        public decimal? CostoRealServiciosTerceros { get; set; }
        public decimal? CostoRealCostosIndirectos { get; set; }
    }
}