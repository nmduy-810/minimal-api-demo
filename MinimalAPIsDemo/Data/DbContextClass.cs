using Microsoft.EntityFrameworkCore;
using MinimalAPIsDemo.Entities;

namespace MinimalAPIsDemo.Data;

public class DbContextClass : DbContext
{
    private readonly IConfiguration _configuration;

    public DbContextClass(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Product> Product { get; set; } = default!;
}