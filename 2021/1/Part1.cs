using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._1;

public class Part1 : AoCPart
{
    public override object Run()
    {
        var prev         = float.MaxValue;
        var greaterCount = 0;
        var lines        = InputLines();
        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var val  = int.Parse(line);
            if (val > prev)
            {
                greaterCount++;
            }

            prev = val;
        }

        return greaterCount;
    }
}