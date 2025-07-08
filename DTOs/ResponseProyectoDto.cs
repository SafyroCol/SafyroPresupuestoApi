using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.DTOs
{
    public class ResponseProyectoDto
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public Proyecto Contenido { get; set; }
        public List<Proyecto> ContenidoList { get; set; }
    }
}
