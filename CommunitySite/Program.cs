using CommunitySite.CommunitySiteEntities;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudBlazor.Components;
using CommunitySite.Extensions;
using CommunitySite.Components.Accessories;
using CommunitySite.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.RegisterDatabaseContexts(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

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

app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
