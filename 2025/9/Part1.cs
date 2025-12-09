using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._9;

public class Part1 : AoCPart
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
         """, "50")
    ];

    public override object Run(string input)
    {
        List<Vector2> tiles = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(",");
            tiles.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }

        long maxArea = 0;
        foreach (var tileA in tiles)
        {
            foreach (var tileB in tiles)
            {
                var x    = (long) Math.Abs(tileA.X - tileB.X) + 1;
                var y    = (long) Math.Abs(tileA.Y - tileB.Y) + 1;
                var area = x * y;
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }

        return maxArea;
    }
}