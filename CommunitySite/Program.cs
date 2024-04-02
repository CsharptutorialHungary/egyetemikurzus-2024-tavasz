using MudBlazor.Services;
using CommunitySite.Extensions;
using CommunitySite.Components;
using System.Reflection;
using CommunitySite.Extensions.Mapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.RegisterDatabaseContexts(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
