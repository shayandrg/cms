var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

builder.Services.AddControllersWithViews();

builder.AddDatabase();

var app = builder.Build();

app.AddDevelopmentMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapEndpoints();

app.SeedData();

app.Run();