using Microsoft.EntityFrameworkCore;
using Simplz.DNDBeyondOpenAI.Bot.Components;
using Simplz.DNDBeyondOpenAI.Bot.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<GameContext>(o => o.UseSqlite($"Data Source={Path.Join("Temp", "Game.db")}"));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddBlazorBootstrap();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

await using var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

await app.RunAsync();
