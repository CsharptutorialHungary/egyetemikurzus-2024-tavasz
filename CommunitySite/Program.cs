using CommunitySite.CommunitySiteEntities;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using CommunitySite.Components.Layout;
using CommunitySite.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ModelContext>(o =>
{
    o.UseOracle(connectionString);
});

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

app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapBlazorHub();
app.Run();
