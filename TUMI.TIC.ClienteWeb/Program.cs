using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;
using TUMI.TIC.Data;
using TUMI.TIC.Dominio;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Repositorio.Contratos;
using TUMI.TIC.Repositorio.SqlServer;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-PE");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-PE");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new Conexion(builder.Configuration.GetConnectionString("DefaultConnection")));

//Inyección de Dependencias (Interface de Repositorio - Repositorio | Interfaz de Dominio - Dominio)
builder.Services.AddScoped<IDashBoardRepositorio, DashBoardRepositorio>();
builder.Services.AddScoped<IDashBoardDominio, DashBoardDominio>();
builder.Services.AddScoped<IGenericoRepositorio, GenericoRepositorio>();
builder.Services.AddScoped<IGenericoDominio, GenericoDominio>();
builder.Services.AddScoped<IColaboradorRepositorio, ColaboradorRepositorio>();
builder.Services.AddScoped<IColaboradorDominio, ColaboradorDominio>();
builder.Services.AddScoped<IOficinaRepositorio, OficinaRepositorio>();
builder.Services.AddScoped<IOficinaDominio, OficinaDominio>();
builder.Services.AddScoped<IAreaRepositorio, AreaRepositorio>();
builder.Services.AddScoped<IAreaDominio, AreaDominio>();
builder.Services.AddScoped<ICargoRepositorio, CargoRepositorio>();
builder.Services.AddScoped<ICargoDominio, CargoDominio>();
builder.Services.AddScoped<ITicketRepositorio, TicketRepositorio>();
builder.Services.AddScoped<ITicketDominio, TicketDominio>();

// Configurar autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Ruta de login
        options.LogoutPath = "/Portal/Logout"; // Ruta de logout
    });

//Cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Configuración del límite de tamaño de formulario
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // Esto permite subir archivos de cualquier tamaño
});

//termino de inyección de dependencias
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
