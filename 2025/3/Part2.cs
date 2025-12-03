using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._3;

public class Part2 : AoCPart
{
    public override object Run()
    {
        long joltageSum = 0;
        foreach (var bank in InputLines())
        {
            var batteries = bank.ToList().Select(c => int.Parse(c.ToString())).ToList();

            var canRemove = batteries.Count - 12;

            var leave = false;
            while (canRemove > 0 && !leave)
            {
                for (int i = 1; i < batteries.Count; i++)
                {
                    if (canRemove > 0 && batteries[i] > batteries[i - 1])
                    {
                        batteries.RemoveAt(i - 1);
                        canRemove--;
                        break;
                    }

                    if (i == batteries.Count - 1)
                    {
                        leave = true;
                        break;
                    }
                }
            }

            while (canRemove > 0)
            {
                batteries.RemoveAt(batteries.IndexOf(batteries.Min()));
                canRemove--;
            }

            var bankJoltage = long.Parse(string.Join("", batteries));
            joltageSum += bankJoltage;
        }
        return joltageSum;
    }
}