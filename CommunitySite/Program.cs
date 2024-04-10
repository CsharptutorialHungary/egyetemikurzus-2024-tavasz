using MudBlazor.Services;
using CommunitySite.Extensions;
using MudBlazor.Components;
using CommunitySite.Extensions;
using CommunitySite.Components.Accessories;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MudBlazor.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using CommunitySite.Data.ViewModels;
using CommunitySite.Data.Entities;
using CommunitySite.Services.UserServices;
using CommunitySite.Data.ViewModels;
using CommunitySite.Data.Entities;
using CommunitySite.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.RegisterDatabaseContexts(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.RegisterApplicationServices();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string baseHref = builder.Configuration.GetValue<string>("Base") ?? "";

var app = builder.Build();

app.UsePathBase($"/{baseHref}/");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseSession();
app.MapDefaultControllerRoute();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
