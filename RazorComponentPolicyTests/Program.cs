using Microsoft.AspNetCore.Authorization;
using RazorComponentPolicyTests.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policy.UserPolicy, policy =>
    {
        policy.Requirements.Add(new Requirements.UserPolicy());
    });
    config.AddPolicy(Policy.AdminPolicy, policy =>
    {
        policy.Requirements.Add(new Requirements.AdminPolicy());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, Handlers.UserHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, Handlers.AdminHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();