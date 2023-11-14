﻿// using Microsoft.EntityFrameworkCore;
// using Moq;
//
// namespace xUnitTest;
//
// public static class MockExtensions
// {
//     public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
//     {
//         var mockSet = new Mock<DbSet<T>>();
//
//         mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
//         mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
//         mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
//         mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
//
//         return mockSet;
//     }
// }