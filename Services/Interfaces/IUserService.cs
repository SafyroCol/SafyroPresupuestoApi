using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto?> AuthenticateUserAsync(LoginDto loginDto);
    }
}
