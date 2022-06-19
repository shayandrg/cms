using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyCms.DataLayer.Context;
using MyCms.Web.Models;

namespace MyCms.Web.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var cnn ="Server=localhost;Port=49597;Database=cms_db;User Id=dbUser;Password=pass;";
            optionsBuilder.UseNpgsql(cnn);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}