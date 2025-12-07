using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._5;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests => [("11-11\n11-13\n13-15\n11-11\n5-11\n10-11", "11")];

    public override object Run(string input)
    {
        long               fresh    = 0;
        List<(long, long)> freshIds = [];
        foreach (var line in SplitInput(input, false))
        {
            if (line == "")
            {
                break;
            }

            var split = line.Split("-");
            var start = long.Parse(split[0]);
            var until = long.Parse(split[1]);

            while (freshIds.TryGetFirst(x => x.Item1 <= start && x.Item2 >= start, out var range))
            {
                start = range.Item2 + 1;
            }

            while (freshIds.TryGetFirst(x => x.Item1 <= until && x.Item2 >= until, out var range))
            {
                until = range.Item1 - 1;
            }

            if (until < start)
            {
                continue;
            }

            while (freshIds.TryGetFirst(x => x.Item1 >= start && x.Item2 <= until, out var skipped))
            {
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