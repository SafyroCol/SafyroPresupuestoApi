using System.Collections.Generic;

namespace SafyroPresupuestos.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}