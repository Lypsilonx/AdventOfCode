using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._4;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.",
         "43")
    ];

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

    // private static int _sizeX;
    // private static int _sizeY;
    // 
    // public override string TestInput =>
    //     "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
    // 
    // public override string TestSolution => "43";
    // 
    // public override object Run()
    // {
    //     var lines = InputLines();
    //     _sizeX = lines.Length;
    //     _sizeY = lines[0].Length;
    //     var grid = new bool[_sizeX, _sizeY];
    // 
    //     var x = 0;
    //     foreach (var line in lines)
    //     {
    //         var y = 0;
    //         foreach (var c in line)
    //         {
    //             grid[x, y] = c == '@';
    //             y++;
    //         }
    // 
    //         x++;
    //     }
    // 
    //     var  removed = 0;
    //     bool changed;
    //     do
    //     {
    //         changed = false;
    //         for (x = 0; x < _sizeX; x++)
    //         {
    //             for (var y = 0; y < _sizeY; y++)
    //             {
    //                 if (!grid[x, y])
    //                 {
    //                     continue;
    //                 }
    // 
    //                 if (!CanRemoveRoll(grid, x, y))
    //                 {
    //                     continue;
    //                 }
    // 
    //                 removed++;
    //                 grid[x, y] = false;
    //                 changed    = true;
    //             }
    //         }
    //     } while (changed);
    // 
    //     return removed;
    // }
    // 
    // private static bool CanRemoveRoll(bool[,] grid, int x, int y)
    // {
    //     var surrounding    = 0;
    //     var nonSurrounding = 0;
    // 
    //     var border1 = x == _sizeX - 1;
    //     var border2 = y == _sizeY - 1;
    //     var border3 = x == 0;
    //     var border4 = y == 0;
    // 
    //     var boundX = border1
    //                      ? 1
    //                      : 2;
    //     var boundY = border2
    //                      ? 1
    //                      : 2;
    // 
    //     if ((border1 || border3) && (border2 || border4))
    //     {
    //         return true;
    //     }
    // 
    //     for (var xd = border3
    //                       ? 0
    //                       : -1;
    //          xd < boundX;
    //          xd++)
    //     {
    //         for (var yd = border4
    //                           ? 0
    //                           : -1;
    //              yd < boundY;
    //              yd++)
    //         {
    //             if (grid[x + xd, y + yd])
    //             {
    //                 if (surrounding == 4)
    //                 {
    //                     return false;
    //                 }
    //     
    //                 surrounding++;
    //             }
    //             else
    //             {
    //                 if (nonSurrounding
    //                     == (border1 || border2 || border3 || border4
    //                             ? 1
    //                             : 4))
    //                 {
    //                     return true;
    //                 }
    //     
    //                 nonSurrounding++;
    //             }
    //         }
    //     }
    // 
    //     return true;
    // }
}