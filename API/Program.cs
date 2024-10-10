using API.Extensiones;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Agregar servicios al contenedor.
builder.Services.AddControllers();

builder.Services.AgregaServiciosAplicacion(builder.Configuration);
builder.Services.AgregaServiciosIdentidad(builder.Configuration);


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AplicationDbContext>(opciones =>
//    opciones.UseSqlServer(connectionString)
//);

//// Configurar CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("PermitirTodo", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});


builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//builder.Services.AddSwaggerGen(options =>
//{
//    // Definir el esquema de seguridad para Bearer
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Autorización JWT utilizando el esquema Bearer. \r\n\r\n Introduce 'Bearer' [espacio] y luego el token.\r\n\r\nEjemplo: 'Bearer abc123xyz'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer"
//    });

//    // Aplicar seguridad globalmente en todos los endpoints
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In = ParameterLocation.Header
//            },
//            new List<string>()
//        }
//    });
//});


//builder.Services.AddScoped<ITokenServicio, TokenServicio>(); // Se agrega Servicio
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) /// servicio de AUTENTICACION
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
//            ValidateIssuer = false,
//            ValidateAudience = false,
//        };
//    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();//Manejo de Error Handling
app.UseStatusCodePagesWithReExecute("/errores/{0}"); //Cuando el controlador(ErrorController) no tiene metodo get

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//.WithOpenApi();

// Aplicar política de CORS
app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthentication(); /// AUTENTICACION
app.UseAuthorization();

app.MapControllers();

app.Run();


