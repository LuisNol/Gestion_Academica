using Microsoft.EntityFrameworkCore;
using Parcial3_sumaran.Models;

var builder = WebApplication.CreateBuilder(args);

// 👇 Esto es clave para Render
builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddDbContext<GestionAcademicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDb")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Redirección a Swagger
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⚠️ Omitir si no necesitas HTTPS dentro del contenedor
// app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
