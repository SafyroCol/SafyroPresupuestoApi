using System;

namespace SafyroPresupuestos.Services.Interfaces
{
	public interface ITenantAccessor
	{
		Guid EmpresaId { get; set; }
	}
}
