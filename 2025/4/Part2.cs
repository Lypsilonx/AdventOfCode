using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._4;

public class Part2 : AoCPart
{
    public override object Run()
    {
        List<Vector2> grid  = [];
        var           lines = InputLines();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '@')
                {
                    grid.Add(new Vector2(x, y));
                }
            }
        }

        var removed = 0;
        while (true)
        {
            List<Vector2> removable = [];
            foreach (var roll in grid)
            {
                var surrounding = 0;
                for (int xd = -1; xd < 2; xd++)
                {
                    for (int yd = -1; yd < 2; yd++)
                    {
                        if (xd == 0 && yd == 0)
                        {
                            continue;
                        }
                        if (grid.Contains(new Vector2(roll.X + xd, roll.Y + yd)))
                        {
                            surrounding++;
                        }
                    }
                }
                if (surrounding < 4)
                {
                    removable.Add(roll);
                }
            }

            if (removable.Count == 0)
            {
                break;
            }

            removed += removable.Count;

            foreach (var remove in removable)
            {
                grid.Remove(remove);
            }
        }

        return removed;
    }
}