
using Microsoft.EntityFrameworkCore;
using GestorClientes.Application.Mapping;
using GestorClientes.Application.Services;
using GestorClientes.Infrastructure.Data;
using GestorClientes.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configuración de controladores y vistas MVC
builder.Services.AddControllersWithViews();

// Conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();

// Configuración del manejo de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Configuración de rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
