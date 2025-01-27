using System.Data;
using Microsoft.Data.SqlClient;
using FurnitureEmporium.Models.Interface;
using FurnitureEmporium.Models.Repository;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure the database connection and repository services
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserinterface, UserRepository>();

builder.Services.AddScoped<AuthenticationStateProvider, FurnitureEmporium.Authentication.CustomAuthenticationStateProvider>();
builder.Services.AddAuthenticationCore();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
