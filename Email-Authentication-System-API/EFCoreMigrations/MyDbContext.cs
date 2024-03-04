using Microsoft.EntityFrameworkCore;
using Model.Entitys;

namespace EFCoreMigrations;

public class MyDbContext : DbContext
{
    public DbSet<users> users { get; set; }


    //注入方式配置
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}