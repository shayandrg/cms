using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.Web.Data;
using MyCms.Web.Models;

namespace Cms.Presentation.Configuration;

/// <summary>
/// Some extension methods for 'WebApplicationbuilder'
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add database configuration based on environment
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        var cnn ="Server=localhost;Port=49597;Database=cms_db;User Id=dbUser;Password=pass;";
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(cnn));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddDbContext<MyCmsDbContext>(options =>
        {
            options.UseNpgsql(cnn).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            options.LogTo(Console.WriteLine);
        });
    }
}