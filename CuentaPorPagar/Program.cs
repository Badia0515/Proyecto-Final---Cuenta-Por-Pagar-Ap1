using CuentaPorPagar.Components;
using CuentasPorPagar.Data;
using CuentasPorPagar.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var Conexion = builder.Configuration.GetConnectionString("Conexion");
builder.Services.AddDbContextFactory<ContextBD>(options => options.UseSqlite(Conexion));
builder.Services.AddScoped<DevolucionesService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<PagosService>();
builder.Services.AddScoped<ProveedoresService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
