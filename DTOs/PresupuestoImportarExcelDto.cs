namespace SafyroPresupuestos.DTOs
{
    public class PresupuestoImportarExcelDto
    {
        public PresupuestoDto Presupuesto { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
    }

}
