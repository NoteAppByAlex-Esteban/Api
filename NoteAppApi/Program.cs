global using Microsoft.EntityFrameworkCore;
global using NoteApp.Shared.Models;
global using NoteApp.Shared.Enums;
global using Microsoft.AspNetCore.Mvc;
global using NoteAppApi;
global using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using NoteAppApi.Data;

var builder = WebApplication.CreateBuilder(args);


// Politica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Obtiene el string de conexion
string sql = builder.Configuration["ConnectionStrings:Release"] ?? "";
if (sql.Length > 0)
{
    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseSqlServer(sql);
    });
}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS vacio
app.UseCors("AllowAnyOrigin");

// Trata de crear la base de datos
try
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<Context>();
    var res = dataContext.Database.EnsureCreated();
}
catch
{ }


// Usar Swagger
app.UseSwagger();
app.UseSwaggerUI();


// Ruta de RealTime (Notas)
app.MapHub<NoteAppApi.Hubs.NoteRealTime>("/realtime/notes");

// Establece el string de conexion SQL
Conexion.SetString(sql);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
