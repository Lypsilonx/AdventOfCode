using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._10;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
         [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
         [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
         """, "7"),
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
            var buttonCount = 0;

            var found = false;
            var lsList = machine.LightStates.Enumerate()
                                .Where(ls => ls.Value)
                                .Select(ls => ls.Index)
                                .ToList();
            while (!found)
            {
                buttonCount++;
                var combinations = machine.ButtonCombinations(buttonCount);
                foreach (var combination in combinations)
                {
                    if (combination.SequenceEqual(lsList))
                    {
                        found = true;
                        Console.WriteLine(buttonCount);
                        break;
                    }
                }
            }

            presses += buttonCount;
        }
        return presses;
    }
}