using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._6;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  ", "4277556")
    ];

    public override object Run(string input)
    {
        long             result  = 0;
        List<List<long>> columns = [];
        foreach (var line in SplitInput(input))
        {
            var oneSpaceLine = line;
            oneSpaceLine = oneSpaceLine.ReplaceRecursive("  ", " ");

            var x = 0;
            foreach (var num in oneSpaceLine.Split(" "))
            {
                switch (num)
                {
                    case "":
                        continue;
                    case "+":
                        result += columns[x]
                            .Sum();
                        x++;
                        continue;
                    case "*":
                        result += columns[x]
                            .Mult();
                        x++;
                        continue;
                }

                if (columns.Count < x + 1)
                {
                    columns.Add([]);
                }

                columns[x]
                    .Add(long.Parse(num));
                x++;
            }
        }

        return result;
    }
}