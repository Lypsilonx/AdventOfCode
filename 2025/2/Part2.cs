using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._2;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
        ("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124",
         "4174379265")
    ];

    public override object Run(string input)
    {
        List<long> invalidIds = [];
        foreach (var idRange in input.Split(","))
        {
            var ids      = idRange.Split("-");
            var firstId  = long.Parse(ids[0]);
            var secondId = long.Parse(ids[1]);

            for (var id = firstId; id <= secondId; id++)
            {
                if (!IsValidId(id))
                {
                    invalidIds.Add(id);
                }
            }
        }

        return invalidIds.Sum();
    }

    private static bool IsValidId(long id)
    {
        var idString = id.ToString();

        for (var split = 1; split <= idString.Length / 2; split++)
        {
            if (idString.Length % split != 0)
            {
                continue;
            }

            var parts = idString.SplitByLength(split);
            if (parts.Distinct()
                     .ToList()
                     .Count
                == 1)
            {
                return false;
            }
        }

        return true;
    }
}