using CommonLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vizsgafeladat.Components;
using Vizsgafeladat.services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44324/") });
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddSingleton<AuthStateService>();  // A felhasználó állapotát jelzi


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// A renderelési mód engedélyezése
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); 

app.Run();

