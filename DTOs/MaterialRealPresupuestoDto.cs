namespace SafyroPresupuestos.DTOs
{
    public class MaterialRealPresupuestoDto
    {
        public int MaterialPresupuestoId { get; set; }
        public string NombreMaterial { get; set; }
        public decimal CostoPresupuestado { get; set; }
        public decimal? CostoReal { get; set; }
    }

}
