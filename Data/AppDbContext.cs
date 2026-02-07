using Microsoft.EntityFrameworkCore;
using TaskDispatcherApi.Models;

namespace TaskDispatcherApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}