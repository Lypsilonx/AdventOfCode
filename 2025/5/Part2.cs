using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._5;

public class Part2 : AoCPart
{
    public override string TestInput    => "11-11\n11-13\n13-15\n11-11\n5-11\n10-11";
    public override string TestSolution => "11";

    public override object Run()
    {
        long               fresh    = 0;
        List<(long, long)> freshIds = [];
        foreach (var line in InputLines(false))
        {
            if (line == "")
            {
                break;
            }

            var split = line.Split("-");
            var start = long.Parse(split[0]);
            var until = long.Parse(split[1]);

            while (freshIds.Any(x => x.Item1 <= start && x.Item2 >= start))
            {
                var startId = freshIds.First(x => x.Item1 <= start && x.Item2 >= start)
                                      .Item2;
                start = startId + 1;
            }

            while (freshIds.Any(x => x.Item1 <= until && x.Item2 >= until))
            {
                var untilId = freshIds.First(x => x.Item1 <= until && x.Item2 >= until)
                                      .Item1;
                until = untilId - 1;
            }

            if (until < start)
            {
                continue;
            }

            while (freshIds.Any(x => x.Item1 >= start && x.Item2 <= until))
            {
                var skipped = freshIds.First(x => x.Item1 >= start && x.Item2 <= until);
                freshIds.Remove(skipped);
                var sub = skipped.Item2 + 1 - skipped.Item1;
                fresh -= sub;
            }

            freshIds.Add((start, until));

            var add = until - start + 1;
            fresh += add;
        }

        return fresh;
    }
}