namespace SafyroPresupuestos.DTOs
{
    public class MaterialConCostoDto
    {
        public Guid MaterialPresupuestoId { get; set; }
        public string NombreMaterial { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitarioPresupuestado { get; set; }
        public decimal CostoTotalPresupuestado { get; set; }
        public decimal? CostoReal { get; set; }
    }

}
