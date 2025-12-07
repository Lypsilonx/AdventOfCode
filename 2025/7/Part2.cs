using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests => [
        (".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............",
         "40"),
    ];
    
    public override object Run()
    {
        var           lines         = InputLines();
        Dictionary<int, long> beamPositions = [];
        long           splitTimes    = 1;
        foreach (var line in lines)
        {
            if (beamPositions.Count == 0)
            {
                beamPositions[line.IndexOf("S")] = 1;
            }
            else
            {
                for (int i = 0; i < line.Length; i++)
                {
                    var c = line[i];
                    switch (c)
                    {
                        case '.':
                            continue;
                        case '^':
                            if (beamPositions.ContainsKey(i))
                            {
                                var amount = beamPositions[i];
                                beamPositions.Remove(i);
                                if (!beamPositions.ContainsKey(i + 1))
                                {
                                    beamPositions.Add(i + 1, amount);
                                }
                                else
                                {
                                    beamPositions[i + 1] += amount;
                                }
                                if (!beamPositions.ContainsKey(i - 1))
                                {
                                    beamPositions.Add(i - 1, amount);
                                }
                                else
                                {
                                    beamPositions[i - 1] += amount;
                                }
                                splitTimes += amount;
                            }
                            continue;
                    }
                }
            }
        }
        return splitTimes;
    }
}