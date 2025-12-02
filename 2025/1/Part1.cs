using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._1;

public class Part1 : AoCPart
{
    public override object Run()
    {
        var dial              = 50;
        var leftPointingCount = 0;
        foreach (var line in InputLines())
        {
            var dir = line[..1];
            var num = int.Parse(line[1..]);
            if (dir == "L")
            {
                dial = (dial - num) % 100;
            }
            else
            {
                dial = (dial + num) % 100;
            }

            if (dial == 0)
            {
                leftPointingCount++;
            }
        }

        return leftPointingCount;
    }
}