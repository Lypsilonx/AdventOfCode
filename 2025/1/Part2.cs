using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._1;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("""
         L68
         L30
         R48
         L5
         R60
         L55
         L1
         L99
         R14
         L82
         """, "6")
    ];

    public override object Run(string input)
    {
        var dial              = 50;
        var leftPointingCount = 0;
        foreach (var line in SplitInput(input))
        {
            var dir = line[..1];
            var num = int.Parse(line[1..]);
            if (dir == "L")
            {
                if (dial > 0 && dial - num <= 0)
                {
                    leftPointingCount++;
                    num  -= dial;
                    dial =  0;
                }

                leftPointingCount += num / 100;

                dial = (dial - num + 10000) % 100;
            }
            else
            {
                if (dial != 0 && dial + num >= 100)
                {
                    leftPointingCount++;
                    num  -= 100 - dial;
                    dial =  0;
                }

                leftPointingCount += num / 100;

                dial = (dial + num + 10000) % 100;
            }
        }

        return leftPointingCount;
    }
}