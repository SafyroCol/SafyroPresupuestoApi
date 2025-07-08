using Microsoft.AspNetCore.Identity;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.DTOs;
using System.Threading.Tasks;
using System;

namespace SafyroPresupuestos.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(LoginDto dto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> AuthenticateAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new Exception("Usuario o contraseña incorrectos.");

            // Aquí puedes generar el JWT
            return "TOKEN_GENERADO_AQUI";
        }

        public async Task<string> RegisterAsync(RegisterDto model)
        {
            var usuario = new Usuario
            {
                UserName = model.Username,
                Email = model.Email,
                EmpresaId = model.EmpresaId
            };

            var resultado = await _userManager.CreateAsync(usuario, model.Password);

            if (!resultado.Succeeded)
            {
                var errores = string.Join("; ", resultado.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo registrar el usuario: {errores}");
            }

            // Crear el rol si no existe
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            await _userManager.AddToRoleAsync(usuario, model.Role);

            return "Usuario registrado correctamente.";
        }

    }
}
