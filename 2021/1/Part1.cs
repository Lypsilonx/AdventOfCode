using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._1;

public class Part1 : AoCPart
{
    public override object Run(string input)
    {
        var prev         = float.MaxValue;
        var greaterCount = 0;
        foreach (var line in SplitInput(input))
        {
            var val = int.Parse(line);
            if (val > prev)
            {
                greaterCount++;
            }

            prev = val;
        }

        return greaterCount;
    }
}