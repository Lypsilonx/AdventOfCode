using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2021._2;

public class Part2 : AoCPart
{
    public override object Run(string input)
    {
        var position = Vector2.Zero;
        var aim      = 0;

        foreach (var line in SplitInput(input))
        {
            var split     = line.Split(" ");
            var direction = split[0];
            var amount    = int.Parse(split[1]);

            switch (direction)
            {
                case "up":
                    aim -= amount;
                    break;
                case "down":
                    aim += amount;
                    break;
                case "forward":
                    position += new Vector2(amount, aim * amount);
                    break;
            }
        }

        return (long) position.X * (long) position.Y;
    }
}