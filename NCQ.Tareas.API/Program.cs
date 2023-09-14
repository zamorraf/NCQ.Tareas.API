using Microsoft.EntityFrameworkCore;
using NCQ.Tareas.API.Datos;
using NCQ.Tareas.API.Datos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// inyectar la conexion al DbContext
builder.Services.AddDbContext<DatosContext>(x => {
    //x.UseLazyLoadingProxies();
    x.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

builder.Services.AddControllers();

// Se agrega el servicio del API para usar en los controladores y poder inyectarlo
builder.Services.AddScoped<IApiRepositorio, ApiRepositorio>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
