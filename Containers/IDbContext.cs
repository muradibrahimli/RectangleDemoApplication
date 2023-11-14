using Microsoft.EntityFrameworkCore;

namespace Containers;

public interface IDbContext
{
    DbSet<Rectangle> Rectangles { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}