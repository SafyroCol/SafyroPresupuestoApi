using SafyroPresupuestos.Services.Interfaces;
using System;

namespace SafyroPresupuestos.Services
{
	public class TenantAccessor : ITenantAccessor
	{
		public Guid EmpresaId { get; set; }
	}
}
