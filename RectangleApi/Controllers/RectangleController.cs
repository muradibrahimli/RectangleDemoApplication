using Containers;
using Containers.Api;
using Microsoft.AspNetCore.Mvc;

namespace RectangleApi.Controllers;

public class RectangleController : ControllerBase
{
    private readonly InMemoryDbContext _context;

    public RectangleController(InMemoryDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route(UrlPath.Coordinates)]
    [Produces("application/json")]
    public Task<IActionResult> SearchRectangles([FromBody] int[][] coordinates)
    {
        var result = coordinates.Select(coord => _context.Rectangles
            .Where(rect => rect.X <= coord[0] && rect.X + rect.Width >= coord[0]
                                              && rect.Y <= coord[1] && rect.Y + rect.Height >= coord[1])
            .ToList());

        return Task.FromResult<IActionResult>(Ok(result));
    }
}