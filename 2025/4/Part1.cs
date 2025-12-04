using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._4;

public class Part1 : AoCPart
{
    public override string TestInput    => "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
    public override string TestSolution => "13";

    public override object Run()
    {
        List<Vector2> grid  = [];
        var           lines = InputLines();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '@')
                {
                    grid.Add(new Vector2(x, y));
                }
            }
        }

        var accessible = 0;
        var hash       = new HashSet<Vector2>(grid);
        foreach (var roll in grid)
        {
            var surrounding = 0;
            for (var xd = -1; xd < 2; xd++)
            {
                for (var yd = -1; yd < 2; yd++)
                {
                    if (xd == 0 && yd == 0)
                    {
                        continue;
                    }

                    if (hash.Contains(new Vector2(roll.X + xd, roll.Y + yd)))
                    {
                        surrounding++;
                    }
                }
            }

            if (surrounding < 4)
            {
                accessible++;
            }
        }

        return accessible;
    }
}