using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._9;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("""
         7,1
         11,1
         11,7
         9,7
         9,5
         2,5
         2,3
         7,3
         """, "24")
    ];

    public override object Run(string input)
    {
        List<Vector2> tiles = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(",");
            tiles.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }

        var maxX        = (int) tiles.Max(v => v.X) + 2;
        var minX        = (int) tiles.Min(v => v.X);
        var maxY        = (int) tiles.Max(v => v.Y) + 2;
        var active      = new bool[maxY];
        var activeTiles = new bool[maxX][];

        List<int> lastXTiles = [];
        for (var x = minX; x < maxX; x++)
        {
            while (lastXTiles.Count > 0)
            {
                var start = lastXTiles[0];
                var end   = lastXTiles[1];

                lastXTiles = lastXTiles.Skip(2)
                                       .ToList();

                if (active[start - 1])
                {
                    start++;
                }

                if (active[end + 1])
                {
                    end--;
                }

                for (var y = start; y < end + 1; y++)
                {
                    active[y] = false;
                }
            }

            var xTiles = tiles.Where(t => t.X == x)
                              .OrderBy(t => t.Y)
                              .ToList();

            while (xTiles.Count > 0)
            {
                var start = (int) xTiles[0].Y;
                var end   = (int) xTiles[1].Y;

                xTiles = xTiles.Skip(2)
                               .ToList();

                var filling = !active[start + 1];

                if (filling)
                {
                    for (var y = start; y < end + 1; y++)
                    {
                        active[y] = true;
                    }
                }
                else
                {
                    lastXTiles.Add(start);
                    lastXTiles.Add(end);
                }
            }

            // Console.WriteLine(active.Select(x => x ? "#" : ".").ToList().Join(""));
            activeTiles[x] = active.Select(x => x)
                                   .ToArray();
        }

        long maxArea = 0;
        foreach (var tileA in tiles)
        {
            foreach (var tileB in tiles)
            {
                if (tiles.Any(t => InSquare(tileA, tileB, t)))
                {
                    continue;
                }

                var x    = (long) Math.Abs(tileA.X - tileB.X) + 1;
                var y    = (long) Math.Abs(tileA.Y - tileB.Y) + 1;
                var area = x * y;
                if (area > maxArea)
                {
                    if (!AllActive(tileA, tileB))
                    {
                        continue;
                    }

                    maxArea = area;
                }
            }
        }

        return maxArea;

        bool InSquare(Vector2 a, Vector2 b, Vector2 point)
        {
            var xMin = Math.Min(a.X, b.X);
            var xMax = Math.Max(a.X, b.X);
            var yMin = Math.Min(a.Y, b.Y);
            var yMax = Math.Max(a.Y, b.Y);
            return point.X > xMin && point.X < xMax && point.Y > yMin && point.Y < yMax;
        }

        bool AllActive(Vector2 a, Vector2 b)
        {
            var xMin = (int) Math.Min(a.X, b.X);
            var xMax = (int) Math.Max(a.X, b.X);
            var yMin = (int) Math.Min(a.Y, b.Y);
            var yMax = (int) Math.Max(a.Y, b.Y);

            for (var xI = xMin; xI < xMax + 1; xI++)
            {
                for (var yI = yMin; yI < yMax + 1; yI++)
                {
                    if (!activeTiles[xI][yI])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}