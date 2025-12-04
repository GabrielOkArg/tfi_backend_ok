using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TFI_Backend.Application.Area.Create;
using TFI_Backend.Application.Area.GetAll;
using TFI_Backend.Application.Area.Update;
using TFI_Backend.Application.Auth.Login;
using TFI_Backend.Application.Auth.Register;
using TFI_Backend.Application.Reclamos;
using TFI_Backend.Application.Reclamos.GatById;
using TFI_Backend.Application.Reclamos.GetByUserId;
using TFI_Backend.Application.Reclamos.GetReclamos;
using TFI_Backend.Application.Reclamos.Update;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Infrastructure.Data;
using TFI_Backend.Infrastructure.Entities;
using TFI_Backend.Infrastructure.Repositories;
using TFI_Backend.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuración de conexión a la base de datos
builder.Services.AddDbContext<GestionReclamosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Inyección de dependencias
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IReclamoRepository, ReclamoRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IreclamoPresupuesto, ReclamoPresupuesto>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<CreateReclamoHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<GetByUserIdQueryHandler>();
builder.Services.AddScoped<GetReclamoByIdQueryHandler>();
builder.Services.AddScoped<UpdateReclamoCommandHandler>();
builder.Services.AddScoped<GetReclamosCommandHandler>();
builder.Services.AddScoped<GetAreaQueryHandler>();
builder.Services.AddScoped<CreateAreaCommandHandler>();
builder.Services.AddScoped<UpdateAreaCommandHandler>();




// 🔹 Configuración JWT
builder.Services.AddAuthentication(options =>
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// 🔹 Controladores
builder.Services.AddControllers();

// 🔹 Configuración Swagger + soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TFI_Backend", Version = "v1" });

    // Configuración de seguridad (Bearer)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido del token JWT.\n\nEjemplo: **Bearer eyJhbGciOi...**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// 🔹 Inicialización de la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GestionReclamosDbContext>();
    DbInitializer.Initialize(context);
}


// 🔹 Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// 🔹 Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
