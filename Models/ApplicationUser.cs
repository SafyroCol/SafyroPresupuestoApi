using Microsoft.AspNetCore.Identity;

namespace SafyroPresupuestos.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int EmpresaId { get; set; } // Relacionado con multiempresa
    }
}
