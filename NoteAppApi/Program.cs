global using Microsoft.EntityFrameworkCore;
global using NoteApp.Shared.Models;
global using NoteApp.Shared.Enums;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore.SqlServer;

using NoteAppApi;
using Microsoft.Data.SqlClient;
using NoteAppApi.Data;

var builder = WebApplication.CreateBuilder(args);


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
app.UseCors("AllowAnyOrigin");
try
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<Context>();
    var res = dataContext.Database.EnsureCreated();
}
catch
{ }



app.UseSwagger();
app.UseSwaggerUI();


Conexion.SetString(sql);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
