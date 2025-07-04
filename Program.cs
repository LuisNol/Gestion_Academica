using Microsoft.EntityFrameworkCore;
using Parcial3_sumaran.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<GestionAcademicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDb")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Redirect root to Swagger UI
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS (must be before UseAuthorization)
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
