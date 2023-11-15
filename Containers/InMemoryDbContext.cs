using Microsoft.EntityFrameworkCore;

namespace Containers;

public class InMemoryDbContext : DbContext, IDbContext
{
    public DbSet<Rectangle> Rectangles { get; set; }
    public DbSet<User> Users { get; set; }


    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add indexes
        modelBuilder.Entity<Rectangle>().HasIndex(r => new { r.X, r.Y });

        base.OnModelCreating(modelBuilder);
    }
}
