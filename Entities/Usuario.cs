using Microsoft.AspNetCore.Identity;
using System;

namespace SafyroPresupuestos.Entities
{
    public class Usuario : IdentityUser
    {
        public int EmpresaId { get; set; }
        public bool Activo { get; set; } = true;
        public Empresa Empresa { get; set; }
        public string Rol { get; internal set; }
    }
}
