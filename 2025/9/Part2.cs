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
        Dictionary<uint, List<uint>> tiles = [];
        uint                         maxX  = 0;
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(",");
            var x     = uint.Parse(split[0]);
            var y     = uint.Parse(split[1]);
            if (tiles.ContainsKey(x))
            {
                tiles[x]
                    .Add(y);
            }
            else
            {
                tiles[x] = [y];
            }

            if (x > maxX)
            {
                maxX = x;
            }
        }

        var  activeTiles = new (uint X, uint Y)[maxX + 1];
        uint start       = 0;
        uint end         = 0;
        foreach (var x in tiles.Keys.OrderBy(x => x))
        {
            var xTiles = tiles[x]
                         .OrderBy(t => t)
                         .ToList();

            if (xTiles.Count > 0)
            {
                if (start == xTiles[0])
                {
                    start = xTiles.Last();
                }
                else if (start == 0 || start == xTiles.Last())
                {
                    start = xTiles[0];
                }

                if (end == 0 || end == xTiles[0])
                {
                    end = xTiles.Last();
                }
                else if (end == xTiles.Last())
                {
                    end = xTiles[0];
                }
            }

            // Console.WriteLine(active.Select(x => x ? "#" : ".").ToList().Join(""));
            activeTiles[x] = (start, end);
        }

        long maxArea = 0;
        foreach (var tileAx in tiles.Keys)
        {
            foreach (var tileAy in tiles[tileAx])
            {
                foreach (var tileBx in tiles.Keys)
                {
                    foreach (var tileBy in tiles[tileBx])
                    {
                        var xMin = Math.Min(tileAx, tileBx);
                        var xMax = Math.Max(tileAx, tileBx);
                        var yMin = Math.Min(tileAy, tileBy);
                        var yMax = Math.Max(tileAy, tileBy);
                        var x    = xMax - xMin + 1;
                        var y    = yMax - yMin + 1;
                        var area = x * y;
                        if (area <= maxArea || !AllActive(xMin, xMax, yMin, yMax))
                        {
                            continue;
                        }

                        maxArea = area;
                    }
                }
            }
        }

        return maxArea;

        bool AllActive(uint xMin, uint xMax, uint yMin, uint yMax)
        {
            return activeTiles[xMax].X    <= yMin
                   && activeTiles[xMin].X <= yMin
                   && activeTiles[xMin].Y >= yMax
                   && activeTiles[xMax].Y >= yMax;
        }
    }
}