using System;
using System.Collections.Generic;

namespace SafyroPresupuestos.Entities
{
    public class TipoEquipo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
		public int EquipoDepreciacionId { get; set; }
		public EquipoDepreciacion Equipo { get; set; }
    }
}