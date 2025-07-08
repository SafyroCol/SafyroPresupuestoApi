using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafyroPresupuestos.Models;
using SafyroPresupuestos.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using SafyroPresupuestos.Services.Interfaces;
using System.Linq;

namespace SafyroPresupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmpresaService _empresaService;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmpresaService empresaService,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _empresaService= empresaService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Los datos de registro no son válidos.");

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmpresaId = model.EmpresaId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            // Asignar rol por defecto (opcional)
            if (!string.IsNullOrEmpty(model.Role))
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Credenciales inválidas.");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("El usuario no existe.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Contraseña incorrecta.");

            if (!string.IsNullOrWhiteSpace(model.Codigo2FA))
            {
                var is2faValid = await _userManager.VerifyTwoFactorTokenAsync(
                    user,
                    _userManager.Options.Tokens.AuthenticatorTokenProvider,
                    model.Codigo2FA
                );

                if (!is2faValid)
                {
                    return Unauthorized("El código de autenticación de dos factores es inválido.");
                }
            }


            var userRoles = await _userManager.GetRolesAsync(user);


            var token = GetToken(user);
            var empresa = await _empresaService.GetByIdAsync(user.EmpresaId);
            var roles = await _userManager.GetRolesAsync(user);

			if (empresa != null)
            {
				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token.Result),
					expiration = token.Result.ValidTo,
					empresaId = empresa.Id,
                    empresaNombre= empresa.Nombre,
					usuario = user.Email,
					roles = roles != null ? roles : [],
				});
			}
			return BadRequest("Empresa no encontrada.");
		}

        private async Task<JwtSecurityToken> GetToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("EmpresaId", user.EmpresaId.ToString()),
                new Claim("uid", user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // 👈 Clave para que [Authorize(Roles = "...")] funcione
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],         // 👈 Debe coincidir con ValidIssuer
                audience: _configuration["Jwt:Issuer"],       // 👈 Debe coincidir con ValidAudience
                expires: DateTime.UtcNow.AddHours(8),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }


        [HttpPost("cambiar-contrasena")]
        public async Task<IActionResult> CambiarContrasena([FromBody] CambiarContrasenaDto model)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (user == null)
                return NotFound("Usuario no encontrado.");

            var result = await _userManager.ChangePasswordAsync(user, model.ContrasenaActual, model.NuevaContrasena);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok("Contraseña cambiada exitosamente.");
        }


        [HttpPost("asignar-rol")]
        [Authorize(Roles = "SuperAdmin,AdministradorEmpresa")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            if (!await _roleManager.RoleExistsAsync(model.Rol))
                await _roleManager.CreateAsync(new IdentityRole(model.Rol));

            var result = await _userManager.AddToRoleAsync(user, model.Rol);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok($"Rol '{model.Rol}' asignado exitosamente al usuario.");
        }

        [HttpGet("roles")]
        public IActionResult ObtenerRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Ok(roles);
        }

        [HttpPost("olvide-contrasena")]
        public async Task<IActionResult> EnviarTokenRecuperacion([FromBody] RecuperarContrasenaDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("No se encontró un usuario con ese correo.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Aquí deberías enviar el token por email en producción.
            return Ok(new
            {
                mensaje = "Se ha enviado un enlace para restablecer la contraseña (simulado).",
                token = token
            });
        }

        [HttpPost("reset-contrasena")]
        public async Task<IActionResult> ResetContrasena([FromBody] ResetContrasenaDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NuevaContrasena);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok("Contraseña restablecida correctamente.");
        }

        [HttpPost("activar-2fa")]
        [AllowAnonymous]
        public async Task<IActionResult> Activar2FA([FromBody] Activar2FADto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
                await _userManager.ResetAuthenticatorKeyAsync(user);

            key = await _userManager.GetAuthenticatorKeyAsync(user);

            return Ok(new
            {
                mensaje = "Escanee este código en su app autenticadora.",
                clave = key,
                qr = $"otpauth://totp/{user.Email}?secret={key}&issuer=Safyro"
            });
        }
    }
}
