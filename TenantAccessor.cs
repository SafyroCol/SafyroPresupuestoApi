using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos
{
	public class TenantAccessor : ITenantAccessor
	{
		public Guid EmpresaId { get; set; }
	}
}

