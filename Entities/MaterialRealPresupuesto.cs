namespace SafyroPresupuestos.Entities
{
    public class MaterialRealPresupuesto
    {
        public Guid Id { get; set; }
        public int PresupuestoId { get; set; }
        public Guid MaterialPresupuestoId { get; set; }
        public decimal CostoReal { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }

}
