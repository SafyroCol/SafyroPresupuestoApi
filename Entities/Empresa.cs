using System;

namespace SafyroPresupuestos.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Dominio { get; set; } = string.Empty;
        public ICollection<Proyecto> Proyectos { get; set; }
    }
}