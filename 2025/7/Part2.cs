using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        (".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............",
         "40")
    ];

    public override object Run()
    {
        Dictionary<int, long> beamPositions = [];
        long                  splitTimes    = 1;
        foreach (var line in InputLines())
        {
            if (beamPositions.Count == 0)
            {
                beamPositions[line.IndexOf('S')] = 1;
            }
            else
            {
                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i];

                    if (c != '^' || !beamPositions.Remove(i, out var amount))
                    {
                        continue;
                    }

                    beamPositions.ForceAdd(i + 1, amount);
                    beamPositions.ForceAdd(i - 1, amount);
                    splitTimes += amount;
                }
            }
        }

        return splitTimes;
    }
}