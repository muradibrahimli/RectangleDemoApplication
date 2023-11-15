using Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RectangleApi.Controllers;
using Xunit;

namespace xUnitTest;

public class RectangleControllerTests
{
    [Fact]
    public async Task SearchRectangles_ReturnsOkResultWithCorrectData()
    {
        var mockContext = new Mock<IDbContext>(); 
        var controller = new RectangleController(mockContext.Object);

        var mockRectangles = new List<Rectangle>
        {
            new() { Id = 1, X = 0, Y = 0, Width = 10, Height = 10 },
            new() { Id = 2, X = 20, Y = 20, Width = 10, Height = 10 },
        }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Rectangle>>();
        mockDbSet.As<IAsyncEnumerable<Rectangle>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Rectangle>(mockRectangles.GetEnumerator()));

        mockDbSet.As<IQueryable<Rectangle>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Rectangle>(mockRectangles.Provider));
        mockDbSet.As<IQueryable<Rectangle>>().Setup(m => m.Expression).Returns(mockRectangles.Expression);
        mockDbSet.As<IQueryable<Rectangle>>().Setup(m => m.ElementType).Returns(mockRectangles.ElementType);

        mockContext.Setup(c => c.Rectangles).Returns(mockDbSet.Object);

        var coordinates = new[] { new[] { 5, 5 }, new[] { 25, 25 } }; // Coordinates that match the rectangles
        var actionResult = await controller.SearchRectangles(coordinates) as OkObjectResult;

        Assert.NotNull(actionResult);
        Assert.IsType<List<List<Rectangle>>>(actionResult.Value);

        var resultList = actionResult.Value as List<List<Rectangle>>;
        Assert.NotNull(resultList);
        Assert.Equal(coordinates.Length, resultList.Count);

        for (var i = 0; i < coordinates.Length; i++)
        {
            Assert.Equal(mockRectangles.Count(rect =>
                rect.X <= coordinates[i][0] && rect.X + rect.Width >= coordinates[i][0]
                && rect.Y <= coordinates[i][1] && rect.Y + rect.Height >= coordinates[i][1]), resultList[i].Count);
        }
    }
}