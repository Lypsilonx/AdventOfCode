using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._1;

public class Part2 : AoCPart
{
    public override object Run()
    {
        var dial              = 50;
        var leftPointingCount = 0;
        foreach (var line in InputLines())
        {
            var dir = line.Substring(0, 1);
            int num = int.Parse(line.Substring(1));
            if (dir == "L")
            {
                if (dial > 0 && (dial - num) <= 0)
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
                if (dial != 0 && (dial + num) >= 100)
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