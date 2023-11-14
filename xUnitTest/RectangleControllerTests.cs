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
public async Task SearchRectangles_ReturnsCorrectRectangles()
{
    // Arrange
    var mockContext = new Mock<InMemoryDbContext>();

    var rectangles = new List<Rectangle>
    {
        new() { Id = 1, X = 0, Y = 0, Width = 10, Height = 5 },
        new() { Id = 2, X = 5, Y = 5, Width = 8, Height = 4 }
    };

    var mockDbSet = rectangles.AsQueryable().BuildMockDbSet();

    mockContext.Setup(c => c.Rectangles).Returns(mockDbSet.Object);

    var controller = new RectangleController(mockContext.Object);

    var coordinates = new int[][] { new[] { 5, 5 }, new[] { 15, 15 } };

    // Act
    var result = await controller.SearchRectangles(coordinates) as OkObjectResult;
    var resultList = result?.Value as IEnumerable<IEnumerable<Rectangle>>;

    // Assert
    Assert.NotNull(resultList);
    Assert.Equal(2, resultList.Count());
}
}

public static class QueryableExtensions
{
    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source)
        where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
        return mockSet;
    }
}