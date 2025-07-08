using SafyroPresupuestos.Services.Interfaces;

public class MultiTenantMiddleware
{
	private readonly RequestDelegate _next;

	public MultiTenantMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context, ITenantAccessor tenantAccessor)
	{
		if (context.Request.Headers.TryGetValue("EmpresaId", out var empresaIdHeader)
			&& Guid.TryParse(empresaIdHeader.FirstOrDefault(), out var empresaId))
		{
			tenantAccessor.EmpresaId = empresaId;
		}

		await _next(context);
	}
}
