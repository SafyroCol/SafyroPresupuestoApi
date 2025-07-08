using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SafyroPresupuestos
{
	public class ValidarEmpresaIdAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var request = context.HttpContext.Request;
			var user = context.HttpContext.User;

			// ⚠️ Evitar que Swagger falle por falta de headers/tokens
			var isSwagger = request.Path.StartsWithSegments("/swagger") ||
							request.Headers["User-Agent"].ToString().Contains("Swagger", StringComparison.OrdinalIgnoreCase);

			if (isSwagger) return;

			if (!user.Identity?.IsAuthenticated ?? false)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			if (!request.Headers.TryGetValue("EmpresaId", out var empresaIdHeader))
			{
				context.Result = new BadRequestObjectResult("El encabezado EmpresaId es obligatorio.");
				return;
			}

			var empresaIdClaim = user.FindFirst("EmpresaId")?.Value;
			if (string.IsNullOrEmpty(empresaIdClaim))
			{
				context.Result = new ForbidResult("Token sin empresa autorizada.");
				return;
			}

			if (!empresaIdClaim.Equals(empresaIdHeader.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				context.Result = new ForbidResult("No tiene acceso a esta empresa.");
				return;
			}
		}
	}

}
