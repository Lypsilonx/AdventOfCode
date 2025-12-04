using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._1;

public class Part1 : AoCPart
{
    public override string TestInput    => "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";
    public override string TestSolution => "3";
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