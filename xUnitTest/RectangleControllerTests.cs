// using Containers;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using RectangleApi.Controllers;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace xUnitTest;
//
// public class RectangleControllerTests 
// {
//     
//     public RectangleControllerTests(ITestOutputHelper output)
//     {
//     }
//
//     [Fact]
//     public async Task SearchRectangles_ReturnsCorrectRectangles()
//     {  
//         // Arrange
//         var rectangles = new List<Rectangle>
//         {
//             new() { Id = 1, X = 0, Y = 0, Width = 10, Height = 5 },
//             new() { Id = 2, X = 5, Y = 5, Width = 8, Height = 4 }
//         };
//
//         var mockDbSet = rectangles.AsQueryable().BuildMockDbSet();
//
//         var mockContext = new Mock<IDbContext>();
//         mockContext.Setup(c => c.Rectangles).Returns(mockDbSet.Object);
//
//     
//         
//         var coordinates = new int[][] { new[] { 5, 5 }, new[] { 15, 15 } };
//  
//         var controller = new RectangleController(mockContext.Object);
//
//
//         // Act
//         var result = await controller.SearchRectangles(coordinates) as OkObjectResult;
//         var resultList = result?.Value as IEnumerable<List<Rectangle>>;
//
//         // Assert
//         // Assert.NotNull(resultList);
//         // Assert.Equal(2, resultList.Count());
//         // Assert.Equal(1, resultList.First().Count); // First coordinate should match one rectangle
//         // Assert.Equal(0, resultList.Last().Count); // Second coordinate should not match any rectangle
//     }
// }
