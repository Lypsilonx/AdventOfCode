using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._3;

public class Part1 : AoCPart
{
    public override object Run()
    {
        var inputLines = InputLines();
        var bits       = new int[inputLines[0].Length];

        foreach (var line in inputLines)
        {
            for (var index = 0; index < line.Length; index++)
            {
                var c = line[index];
                if (c == '1')
                {
                    bits[index]++;
                }
            }
        }

        var gammaBits = bits.Select(x => x > inputLines.Length / 2)
                            .ToList();

        var epsilonBits = gammaBits.Select(x => !x)
                                   .ToList();
        var gamma   = gammaBits.ToInt();
        var epsilon = epsilonBits.ToInt();

        return gamma * epsilon;
    }
}