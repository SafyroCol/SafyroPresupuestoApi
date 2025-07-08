using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Services;
using SafyroPresupuestos.Services.Interfaces;
using System.Security.Claims;

namespace SafyroPresupuestos.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IPresupuestoService _presupuestoService;

        protected BaseController(IPresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }
        protected int EmpresaId
        {
            get
            {
                // 1. Primero intenta extraerlo del claim
                var claim = User?.FindFirst("EmpresaId")?.Value;

                if (int.TryParse(claim, out var empresaGuid))
                    return empresaGuid;

                // 2. Luego intenta obtenerlo del header
                if (Request.Headers.TryGetValue("EmpresaId", out var headerValue) &&
                    int.TryParse(headerValue, out empresaGuid))
                    return empresaGuid;

                throw new UnauthorizedAccessException("No se pudo determinar la empresa asociada a esta solicitud.");
            }
        }

        protected bool ValidarEmpresa(int entidadEmpresaId)
        {
            return entidadEmpresaId == EmpresaId;
        }

        protected bool ValidarEmpresaPorPresupuesto(int presupuestoId)
        {
            var empresaDelUsuario = EmpresaId;
            var empresaDelPresupuesto = _presupuestoService.ObtenerEmpresaPorPresupuesto(presupuestoId);
            
            if(empresaDelPresupuesto != null)
                return empresaDelUsuario == empresaDelPresupuesto.Result;
            return false;
        }
    }
}
