using Microsoft.AspNetCore.Identity.Data;
using SafyroPresupuestos.Models;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponse Authenticate(LoginRequest request);
    }
}