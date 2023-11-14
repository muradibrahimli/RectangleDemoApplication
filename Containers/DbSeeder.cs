﻿namespace Containers;

public abstract class DbSeeder
{
    public static void SeedData(InMemoryDbContext context)
    {
        if (context.Rectangles.Any()) return;
        var random = new Random();
        for (var i = 0; i < 200; i++)
        {
            context.Rectangles.Add(new Rectangle
            {
                X = random.Next(0, 100),
                Y = random.Next(0, 100),
                Width = random.Next(1, 20),
                Height = random.Next(1, 20)
            });
        }

        context.SaveChanges();
    }
}