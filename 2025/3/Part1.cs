using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._3;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("987654321111111\n811111111111119\n234234234234278\n818181911112111", "357")
    ];

    public override object Run(string input)
    {
        long joltageSum = 0;
        foreach (var bank in SplitInput(input))
        {
            var batteries = bank.ToList()
                                .Select(c => int.Parse(c.ToString()))
                                .ToList();

            var max = 0;
            for (var i = 0; i < batteries.Count; i++)
            {
                var num1 = batteries[i];
                for (var j = 0; j < batteries.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var num2 = batteries[j];
                    if (i < j)
                    {
                        var joltage = num1 * 10 + num2;
                        if (joltage > max)
                        {
                            max = joltage;
                        }
                    }
                    else
                    {
                        var joltage = num2 * 10 + num1;
                        if (joltage > max)
                        {
                            max = joltage;
                        }
                    }
                }
            }

            var bankJoltage = max;
            joltageSum += bankJoltage;
        }

        return joltageSum;
    }
}