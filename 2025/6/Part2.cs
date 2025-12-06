using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._6;

public class Part2 : AoCPart
{
    public override string TestInput    => "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  ";
    public override string TestSolution => "3263827";

    public override object Run()
    {
        long             result = 0;
        var              lines  = InputLines();
        List<List<char>> table  = [];
        foreach (var line in lines)
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (table.Count < x + 1)
                {
                    table.Add(new List<char>());
                }

                table[x]
                    .Add(line[x]);
            }
        }

        table.Add([]);

        List<long> numbers = [];
        var        op      = "";
        foreach (var column in table)
        {
            var colStr = column.Join("").Replace(" ", "");

            if (colStr.EndsWith("*"))
            {
                op     = "*";
                colStr = colStr[..^1];
            }
            else if (colStr.EndsWith("+"))
            {
                op     = "+";
                colStr = colStr[..^1];
            }

            if (colStr == "")
            {
                var value = op == "+"
                                ? numbers.Sum()
                                : numbers.Mult();

                result += value;
                op     =  "";
                numbers.Clear();
                continue;
            }

            numbers.Add(long.Parse(colStr));
        }

        return result;
    }
}