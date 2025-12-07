using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        (".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............",
         "40")
    ];

    public override object Run(string input)
    {
        Dictionary<int, long> beamPositions = [];
        long                  splitTimes    = 1;
        foreach (var line in SplitInput(input)
                     .Enumerate())
        {
            if (line.Index % 2 == 1)
            {
                continue;
            }

            if (beamPositions.Count == 0)
            {
                beamPositions[line.Value.IndexOf('S')] = 1;
            }
            else
            {
                var keys = new List<int>(beamPositions.Keys);
                foreach (var beam in keys)
                {
                    if (line.Value[beam] != '^')
                    {
                        continue;
                    }

                    beamPositions.Remove(beam, out var amount);

                    beamPositions.ForceAdd(beam + 1, amount);
                    beamPositions.ForceAdd(beam - 1, amount);
                    splitTimes += amount;
                }
            }
        }

        return splitTimes;
    }
}