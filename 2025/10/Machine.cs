using System.Text.RegularExpressions;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._10;

public class Machine
{
    public bool[]          LightStates;
    private List<List<int>> Buttons  = [];
    public int[]       Joltages;

    public Machine(string input)
    {
        var lights = Regex.Match(input, @"\[([\.\#]+)\]").Groups[1].Value;

        LightStates = new bool[lights.Length];
        for (var i = 0; i < lights.Length; i++)
        {
            LightStates[i] = lights[i] == '#';
        }

        var buttons = Regex.Match(input, @" (\(.*\))").Groups[1].Value[1..^1];

        foreach (var button in buttons.Split(") ("))
        {
            Buttons.Add(button.Split(",").Select(int.Parse).ToList());
        }
        
        var joltages = Regex.Match(input, @" (\{.*\})").Groups[1].Value[1..^1];

        Joltages = new int[LightStates.Length];
        int ji        = 0;
        foreach (var joltage in joltages.Split(","))
        {
            Joltages[ji] = int.Parse(joltage);
            ji++;
        }
    }

    public List<List<int>> ButtonCombinations(int buttonCount)
    {
        List<List<int>> outButtons = new List<List<int>>(Buttons);
        for (int i = 0; i < buttonCount - 1; i++)
        {
            outButtons = CombineButtons(outButtons, new List<List<int>>(Buttons));
        }

        return outButtons;
    }

    private HashSet<int[]> alreadyChecked = [];
    public int CalculateJoltagePresses(int presses = 0, int[] joltagesIn = null)
    {
        if (presses > Joltages.Sum() / Buttons.Min(b => b.Count) + 2)
        {
            return -1;
        }
        
        var modJoltages = new int[Joltages.Length];
        for (var id = 0; id < Joltages.Length; id++)
        {
            modJoltages[id] = (joltagesIn ?? Joltages)[id];
        }

        var firsts = modJoltages.Enumerate()
                                .Where(j => j.Value != 0)
                                .OrderByDescending(j => j.Value).ToList();
        
        firsts = firsts.Where(t => t.Value == firsts.First().Value).ToList();
        if (presses == 0)
        {
            foreach (var first in firsts)
            {
                if (Buttons.All(b => b.Contains(first.Index)))
                {
                    return first.Value;
                }
            }
        }
        var iButtons = Buttons.Where(b => b.Any(firsts.Select(f => f.Index).Contains)
                                          && b.All(id => modJoltages[id] != 0))
                              .OrderByDescending(b => b.Select(id => modJoltages[id]).Sum()).ToList();

        presses++;
        foreach (var button in iButtons)
        {
            foreach (var id in button)
            {
                modJoltages[id]--;
            }

            if (alreadyChecked.Any(ac => ac.SequenceEqual(modJoltages)))
            {
                foreach (var id in button)
                {
                    modJoltages[id]++;
                }
                continue;
            }
            
            if (modJoltages.All(j => j == 0))
            {
                return presses;
            }

            var result = CalculateJoltagePresses(presses, modJoltages);
            if (result != -1)
            {
                return result;
            }

            foreach (var id in button)
            {
                modJoltages[id]++;
            }
        }

        alreadyChecked.Add(modJoltages);
        return -1;
        // != 18973
    }

    
    private List<List<int>> CombineButtons(List<List<int>> a, List<List<int>> b){
        List<List<int>> outButtons = [];
        foreach (var buttonA in a)
        {
            foreach (var buttonB in b)
            {
                if (buttonA == buttonB)
                {
                    continue;
                }

                var intersection = buttonA.Intersect(buttonB);
                var indices      = buttonA.Except(intersection).Concat(buttonB.Except(intersection)).ToList();
                indices.Sort();
                if (indices.Count == 0 || outButtons.Any(l => l.SequenceEqual(indices)))
                {
                    continue;
                }
                
                outButtons.Add(indices);
            }
        }

        return outButtons;
    }
}