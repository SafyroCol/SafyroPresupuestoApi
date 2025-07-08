namespace SafyroPresupuestos.DTOs
{
    public class ResponsePresupuestoDto
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public PresupuestoDto Contenido { get; set; }

    }

}
