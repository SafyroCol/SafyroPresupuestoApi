using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SafyroPresupuestos.Services;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SafyroPresupuestos.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SafyroPresupuestos.Models;
using System.Security.Claims;

namespace SafyroPresupuestos
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // ✅ Eliminar o comentar esta línea:
                    // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

                    // ✅ Usar serialización simple y limpia
                    options.JsonSerializerOptions.ReferenceHandler = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            // Registrar ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),

                    RoleClaimType = ClaimTypes.Role, // 👈 Asegura que .NET sepa cuál claim es el rol
                    NameClaimType = ClaimTypes.Name  // (opcional) para nombres
                };
            });



            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = 401; // Devuelve 401 en vez de redirigir
					return Task.CompletedTask;
				};

				options.Events.OnRedirectToAccessDenied = context =>
				{
					context.Response.StatusCode = 403; // Devuelve 403 si no tiene permiso
					return Task.CompletedTask;
				};
			});


			services.AddScoped<IAuthService, AuthService>();

            // Agregar soporte para controladores


            // Agregar Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SafyroPresupuestos API", Version = "v1" });

                // 🔐 JWT Authentication Configuration
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer abc123\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
            });

            // CORS


            // Inyección de dependencias personalizada
            services.AddScoped<IEmpresaService, EmpresaService>();
			services.AddScoped<ICategoriaMaterialService, CategoriaMaterialService>();
			services.AddScoped<IMonedaService, MonedaService>();
			services.AddScoped<IPermisoUsuarioService, PermisoUsuarioService>();
			services.AddScoped<IResumenFinancieroService, ResumenFinancieroService>();
			services.AddScoped<ITipoEquipoService, TipoEquipoService>();
			services.AddScoped<ITipoManoObraService, TipoManoObraService>();
			services.AddScoped<ITenantAccessor, TenantAccessor>();
			services.AddScoped<IEquipoDepreciacionService, EquipoDepreciacionService>();
			services.AddScoped<IBitacoraPresupuestoService, BitacoraPresupuestoService>();
			services.AddScoped<IEmpresaService, EmpresaService>();
			services.AddScoped<IMaterialService, MaterialService>();
			services.AddScoped<IManoObraService, ManoObraService>();
			services.AddScoped<IPresupuestoService, PresupuestoService>();
			services.AddScoped<IProyectoService, ProyectoService>();
			services.AddHttpContextAccessor(); // si en algún momento lo necesitas
			// Otras configuraciones necesarias aquí
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "SafyroPresupuestos API V1");
					c.RoutePrefix = "swagger"; // ← Deja Swagger en /swagger
				});
			}

         
            app.UseStatusCodePages();
			app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<MultiTenantMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // 👈 esto es obligatorio
            });
        }
	}
}
