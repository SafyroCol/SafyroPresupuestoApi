using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class Moneda
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }

        // Relación uno a muchos
        public ICollection<Presupuestos> Presupuestos { get; set; }
    }
}