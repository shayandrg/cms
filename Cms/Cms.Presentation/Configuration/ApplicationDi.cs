using MyCms.Services.Repositories;
using MyCms.Services.Services;
using MyCms.Web.Services;

namespace Cms.Presentation.Configuration;

public static class ApplicationDi
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IPageRepoitory, PageRepoitory>();
        builder.Services.AddTransient<IPageGroupRepository, PageGroupRepository>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
    }
}