namespace SafyroPresupuestos.DTOs
{
    public class MaterialCostoDto
    {
        public Guid MaterialPresupuestoId { get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitarioPresupuestado { get; set; }
        public decimal TotalPresupuestado => Cantidad * CostoUnitarioPresupuestado;

        public decimal? CostoRealUnitario { get; set; }
        public decimal? TotalReal => CostoRealUnitario.HasValue ? Cantidad * CostoRealUnitario.Value : null;
    }

}
