using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyCms.DataLayer.Context;


public class MyCmsDbContextFactory: IDesignTimeDbContextFactory<MyCmsDbContext>
{
    public MyCmsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyCmsDbContext>();
        var cnn ="Server=localhost;Port=49597;Database=cms_db;User Id=dbUser;Password=pass;";
        optionsBuilder.UseNpgsql(cnn);
        return new MyCmsDbContext(optionsBuilder.Options);
    }
}