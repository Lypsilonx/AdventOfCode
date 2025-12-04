using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._4;

public class Part2 : AoCPart
{
    public override object Run()
    {
        List<Vector2>    grid  = [];
        HashSet<Vector2> hash  = [];
        var              lines = InputLines();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] != '@')
                {
                    continue;
                }

                var vector = new Vector2(x, y);
                grid.Add(vector);
                hash.Add(vector);
            }
        }

        var removed = 0;
        while (true)
        {
            List<Vector2> removable = [];
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
                hash.Remove(remove);
            }

            grid = hash.ToList();
        }

        return removed;
    }
}