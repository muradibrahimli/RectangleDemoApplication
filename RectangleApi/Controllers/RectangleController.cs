using Containers;
using Containers.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RectangleApi.Controllers;

public class RectangleController : ControllerBase
{
    private readonly IDbContext _context;

    public RectangleController(IDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route(UrlPath.Coordinates)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchRectangles([FromBody] int[][] coordinates)
    {
        var result = coordinates.Select(async coord => await _context.Rectangles
            .Where(rect => rect.X <= coord[0] && rect.X + rect.Width >= coord[0]
                                              && rect.Y <= coord[1] && rect.Y + rect.Height >= coord[1])
            .ToListAsync());

        return Ok(await Task.WhenAll(result));
    }
}