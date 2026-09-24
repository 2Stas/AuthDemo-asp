using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> db):base(db) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
}
