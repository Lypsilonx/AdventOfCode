using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests => [
        (".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............",
         "21"),
    ];
    
    public override object Run()
    {
        var       lines         = InputLines();
        List<int> beamPositions = [];
        var       splitTimes    = 0;
        foreach (var line in lines)
        {
            if (beamPositions.Count == 0)
            {
                beamPositions = [line.IndexOf("S")];
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
                            if (beamPositions.Contains(i))
                            {
                                beamPositions.Remove(i);
                                beamPositions.Add(i -1);
                                beamPositions.Add(i +1);
                                beamPositions = beamPositions.Distinct().ToList();
                                splitTimes++;
                            }
                            continue;
                    }
                }
            }
        }
        return splitTimes;
    }
}