using API.Errores;
using BLL.Servicios.Interfaces;
using BLL.Servicios;
using Data;
using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Data.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Utilidades;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {
        public static IServiceCollection AgregaServiciosAplicacion(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                // Definir el esquema de seguridad para Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorización JWT utilizando el esquema Bearer. \r\n\r\n Introduce 'Bearer' [espacio] y luego el token.\r\n\r\nEjemplo: 'Bearer abc123xyz'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                // Aplicar seguridad globalmente en todos los endpoints
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
            });

            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<AplicationDbContext>(opciones =>
                opciones.UseSqlServer(connectionString)
            );

            // Configurar CORS
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("PermitirTodo", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //              .AllowAnyMethod()
            //              .AllowAnyHeader();
            //    });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins", builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //               .AllowAnyMethod()
            //               .AllowAnyHeader();
            //    });
            //});

            services.AddScoped<ITokenServicio, TokenServicio>(); // Se agrega Servicio

            services.Configure<ApiBehaviorOptions>(options =>  //Extencion para manejo de Validacion de Errores
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
           
            services.AddScoped<IEspecialidadRepositorio, EspecialidadRepositorio>();//No convence aun wpineda
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();//patron IUnitofWork

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IEspecialidadServicio, EspecialidadServicio>();

            services.AddScoped<IMedicoServicio, MedicoServicio>();

            return services;
        }
    }
}
