using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._10;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
         [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
         [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
         """, "33"),
    ];
    
    public override object Run(string input)
    {
        List<Machine> machines = [];
        foreach (var line in SplitInput(input))
        {
            machines.Add(new Machine(line));
        }

        var presses = 0;
        foreach (var machine in machines)
        {
            if (machine.Joltages.Length > 6)
            {
                Console.WriteLine("-");
                continue;
            }
            var result = machine.CalculateJoltagePresses();
            if (result == -1)
            {
                Console.WriteLine("-");
            }
            else
            {
                Console.WriteLine(result);
                presses += result;
            }
        }
        return presses;
    }
}

// 138
// 53
// 32
// 151
// -
// 164
// -
// -
// 30
// -
// -
// 155
// 248
// 155
// -
// 47
// -
// -
// -
// -
// -
// -
// -
// -
// 66
// 202
// 5
// 64
