using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        (".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............",
         "21")
    ];

    public override object Run(string input)
    {
        List<int> beamPositions = [];
        var       splitTimes    = 0;
        foreach (var line in SplitInput(input)
                     .Enumerate())
        {
            if (line.Index % 2 == 1)
            {
                continue;
            }

            if (beamPositions.Count == 0)
            {
                beamPositions = [line.Value.IndexOf('S')];
            }
            else
            {
                foreach (var c in line.Value.Enumerate()
                                      .Where(x => x.Value == '^'))
                {
                    if (!beamPositions.Contains(c.Index))
                    {
                        continue;
                    }

                    beamPositions.Remove(c.Index);
                    beamPositions.Add(c.Index - 1);
                    beamPositions.Add(c.Index + 1);
                    beamPositions = beamPositions.Distinct()
                                                 .ToList();
                    splitTimes++;
                }
            }
        }

        return splitTimes;
    }
}