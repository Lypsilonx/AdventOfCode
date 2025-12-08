using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._7;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("""
         .......S.......
         ...............
         .......^.......
         ...............
         ......^.^......
         ...............
         .....^.^.^.....
         ...............
         ....^.^...^....
         ...............
         ...^.^...^.^...
         ...............
         ..^...^.....^..
         ...............
         .^.^.^.^.^...^.
         ...............
         """, "40")
    ];

    public override object Run(string input)
    {
        var  lines         = SplitInput(input);
        var  beamPositions = new long[lines[0].Length];
        long splitTimes    = 1;
        foreach (var line in lines.Enumerate())
        {
            if (line.Index % 2 == 1)
            {
                continue;
            }

            if (line.Index == 0)
            {
                beamPositions[line.Value.IndexOf('S')] = 1;
            }
            else
            {
                for (var i = 0; i < beamPositions.Length; i++)
                {
                    var amount = beamPositions[i];
                    if (line.Value[i] != '^')
                    {
                        continue;
                    }

                    beamPositions[i] = 0;

                    beamPositions[i + 1] += amount;
                    beamPositions[i - 1] += amount;
                    splitTimes           += amount;
                }
            }
        }

        return splitTimes;
    }
}