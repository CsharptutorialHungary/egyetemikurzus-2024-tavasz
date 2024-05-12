using MudBlazor.Services;
using CommunitySite.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;
using CommunitySite.Extensions.Mapper;
using CommunitySite.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//adatbázis regisztrálása
builder.Services.RegisterDatabaseContexts(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddServerSideBlazor();

//snackbar konfiguráció
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 1000;
    config.SnackbarConfiguration.ShowTransitionDuration = 1000;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.NewestOnTop = true;
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
//saját szerviz osztályok regisztrálása
builder.Services.RegisterApplicationServices();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string baseHref = builder.Configuration.GetValue<string>("Base") ?? "";

var app = builder.Build();

app.UsePathBase($"/{baseHref}/");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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

await app.Services.MigrateDatabases();

app.Run();
