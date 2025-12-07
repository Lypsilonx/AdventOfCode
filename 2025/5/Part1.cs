using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._5;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("""
         3-5
         10-14
         16-20
         12-18

         1
         5
         8
         11
         17
         32
         """, "3")
    ];

    public override object Run(string input)
    {
        var                fresh    = 0;
        var                ranges   = true;
        List<(long, long)> freshIds = [];
        foreach (var line in SplitInput(input, false))
        {
            if (line == "")
            {
                ranges = false;
                continue;
            }

            if (ranges)
            {
                var split = line.Split("-");
                var start = long.Parse(split[0]);
                var until = long.Parse(split[1]);
                freshIds.Add((start, until));
            }
            else
            {
                var id = long.Parse(line);
                if (freshIds.Any(x => x.Item1 <= id && x.Item2 >= id))
                {
                    fresh++;
                }
            }
        }

        return fresh;
    }
}