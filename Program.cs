using Microsoft.EntityFrameworkCore;
using P01_2023EQ651_2022CI650.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar la conexión a la base de datos
builder.Services.AddDbContext<ParqueoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Agregar servicios de controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
