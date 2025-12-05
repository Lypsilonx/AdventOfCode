using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._5;

public class Part1 : AoCPart
{
    public override string TestInput    => "3-5\n10-14\n16-20\n12-18\n\n1\n5\n8\n11\n17\n32";
    public override string TestSolution => "3";

    public override object Run()
    {
        var                fresh    = 0;
        var                ranges   = true;
        List<(long, long)> freshIds = [];
        foreach (var line in InputLines(false))
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