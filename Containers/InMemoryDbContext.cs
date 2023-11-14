using Microsoft.EntityFrameworkCore;

namespace Containers;

public class InMemoryDbContext : DbContext
{
    public DbSet<Rectangle> Rectangles { get; set; }

    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
    {
    }
}