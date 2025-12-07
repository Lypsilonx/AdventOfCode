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
        foreach (var line in InputLines().Enumerate())
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
                foreach (var c in line.Value.Enumerate().Where(x => x.Value == '^'))
                {
                    if (!beamPositions.Remove(c.Index, out var amount))
                    {
                        continue;
                    }
                
                    beamPositions.ForceAdd(c.Index + 1, amount);
                    beamPositions.ForceAdd(c.Index - 1, amount);
                    splitTimes += amount;
                }
            }
        }

        return splitTimes;
    }
}