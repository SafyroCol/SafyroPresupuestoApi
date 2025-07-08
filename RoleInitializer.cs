using Microsoft.AspNetCore.Identity;

namespace SafyroPresupuestos
{
    public static class RoleInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[]
            {
                "SuperAdmin",
                "AdministradorEmpresa",
                "CoordinadorDeProyecto",
                "IngenieroDeCostos",
                "SupervisorDeObra",
                "AnalistaFinanciero"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
