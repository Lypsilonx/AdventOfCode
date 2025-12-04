using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._3;

public class Part2 : AoCPart
{
    public override object Run()
    {
        long joltageSum = 0;
        foreach (var bank in InputLines())
        {
            var batteries = bank.ToList()
                                .Select(c => int.Parse(c.ToString()))
                                .ToList();

            var canRemove = batteries.Count - 12;

            var leave = false;
            while (canRemove > 0 && !leave)
            {
                for (var i = 1; i < batteries.Count; i++)
                {
                    if (batteries[i] > batteries[i - 1])
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

            var bankJoltage = long.Parse(batteries.Join(""));
            joltageSum += bankJoltage;
        }

        return joltageSum;
    }

    // public override object Run()
    // {
    //     long joltageSum = 0;
    //     foreach (var bank in InputLines())
    //     {
    //         var batteries = bank.ToList()
    //                             .Select(c => int.Parse(c.ToString()))
    //                             .ToList();
    // 
    //         var canRemove = batteries.Count - 12;
    // 
    //         var start = 0;
    //         while (canRemove > 0)
    //         {
    //             if (start + canRemove + 1 >= batteries.Count)
    //             {
    //                 break;
    //             }
    //             
    //             var maxIndex = batteries[start..(start + canRemove + 1)].IndexOf(
    //                 batteries[start..(start + canRemove + 1)]
    //                     .Max()
    //             );
    //             
    //             if (maxIndex == 0)
    //             {
    //                 start++;
    //                 continue;
    //             }
    //             
    //             batteries.RemoveRange(start, maxIndex);
    //             canRemove -= maxIndex;
    //             start++;
    //         }
    // 
    //         while (canRemove > 0)
    //         {
    //             batteries.RemoveAt(batteries.IndexOf(batteries.Min()));
    //             canRemove--;
    //         }
    // 
    //         var bankJoltage = long.Parse(batteries.Join(""));
    //         joltageSum += bankJoltage;
    //     }
    // 
    //     return joltageSum;
    // }
}