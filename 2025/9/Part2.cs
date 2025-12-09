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
        SortedSet<uint> sxList = [];
        SortedSet<uint> syList = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(",");
            var x     = uint.Parse(split[0]);
            sxList.Add(x);
            var y = uint.Parse(split[1]);
            syList.Add(y);
        }

        var yList = syList.ToList();
        var xList = sxList.ToList();

        var                              layout       = new bool[xList.Count, yList.Count];
        ushort                           lastX        = 0;
        ushort                           lastY        = 0;
        HashSet<(ushort, ushort)>        xConnections = [];
        HashSet<(ushort, ushort)>        yConnections = [];
        Dictionary<ushort, List<ushort>> tiles        = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(",");
            var x     = (ushort) xList.IndexOf(uint.Parse(split[0]));
            var y     = (ushort) yList.IndexOf(uint.Parse(split[1]));
            if (x == lastX)
            {
                var min = Math.Min(y, lastY);
                var max = Math.Max(y, lastY);
                for (var i = (ushort) (min + 1); i < max; i++)
                {
                    layout[x, i] = true;
                }

                yConnections.Add((min, max));
            }
            else if (y == lastY)
            {
                var min = Math.Min(x, lastX);
                var max = Math.Max(x, lastX);
                for (var i = (ushort) (min + 1); i < max; i++)
                {
                    layout[i, y] = true;
                }

                xConnections.Add((min, max));
            }

            lastX = x;
            lastY = y;

            layout[x, y] = true;
            if (!tiles.TryGetValue(x, out var value))
            {
                tiles[x] = [y];
            }
            else
            {
                value.Add(y);
            }
        }

        var prefixLayout = Calculate2DPrefixSum(layout, (ushort) xList.Count, (ushort) yList.Count);

        uint maxArea = 0;
        // List<(ushort X, ushort Y)> marked  = [];
        foreach (var xMax in tiles.Keys)
        {
            foreach (var xMin in tiles.Keys)
            {
                if (xMax <= xMin)
                {
                    continue;
                }

                foreach (var tileAy in tiles[xMax])
                {
                    foreach (var tileBy in tiles[xMin])
                    {
                        var yMin = Math.Min(tileAy, tileBy);
                        var yMax = Math.Max(tileAy, tileBy);
                        var x    = xList[xMax] - xList[xMin] + 1;
                        var y    = yList[yMax] - yList[yMin] + 1;
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

        // for (ushort x = 0; x < xList.Count; x++)
        // {
        //     for (ushort y = 0; y < yList.Count; y++)
        //     {
        //         Console.Write(
        //             // marked.Contains((x, y)) ? "X" :
        //             layout[x, y]
        //                 ? "#"
        //                 : "."
        //         );
        //     }
        // 
        //     Console.WriteLine();
        // }

        return maxArea;

        bool AllActive(ushort xMin, ushort xMax, ushort yMin, ushort yMax)
        {
            if ((xMin + 1 == xMax || yMin + 1 == yMax)
                && !(xConnections.Contains((xMin, xMax)) && yConnections.Contains((yMin, yMax))))
            {
                return false;
            }

            return prefixLayout[xMax - 1, yMax - 1]
                   + prefixLayout[xMin, yMin]
                   - prefixLayout[xMin, yMax - 1]
                   - prefixLayout[xMax       - 1, yMin]
                   == 0;
        }

        static ushort[,] Calculate2DPrefixSum(bool[,] boolMap, ushort lenX, ushort lenY)
        {
            var outMap = new ushort[lenX, lenY];
            for (ushort x = 0; x < lenX; x++)
            {
                for (ushort y = 0; y < lenY; y++)
                {
                    var sum = (ushort) (boolMap[x, y]
                                            ? 1
                                            : 0);
                    if (x != 0)
                    {
                        sum += outMap[x - 1, y];
                    }

                    if (y != 0)
                    {
                        sum += outMap[x, y - 1];
                    }

                    if (x != 0 && y != 0)
                    {
                        sum -= outMap[x - 1, y - 1];
                    }

                    outMap[x, y] = sum;
                }
            }

            return outMap;
        }
    }
}