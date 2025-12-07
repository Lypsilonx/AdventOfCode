using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._1;

public class Part2 : AoCPart
{
    public override object Run(string input)
    {
        var prev         = float.MaxValue;
        var greaterCount = 0;
        var lines        = SplitInput(input);
        for (var index = 2; index < lines.Length; index++)
        {
            var val0 = int.Parse(lines[index - 2]);
            var val1 = int.Parse(lines[index - 1]);
            var val2 = int.Parse(lines[index]);
            var val  = val0 + val1 + val2;
            if (val > prev)
            {
                greaterCount++;
            }

            prev = val;
        }

        return greaterCount;
    }
}