using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._2;

public class Part1 : AoCPart
{
    public override object Run(string input)
    {
        var position = Vector2.Zero;

        foreach (var line in SplitInput(input))
        {
            var split     = line.Split(" ");
            var direction = split[0];
            var amount    = int.Parse(split[1]);

            switch (direction)
            {
                case "up":
                    position += new Vector2(0, -amount);
                    break;
                case "down":
                    position += new Vector2(0, amount);
                    break;
                case "forward":
                    position += new Vector2(amount, 0);
                    break;
            }
        }

        return position.X * position.Y;
    }
}