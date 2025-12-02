using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._2;

public class Part2 : AoCPart
{
    public override object Run()
    {
        List<long> invalidIds = new();
        foreach (var idRange in Input.Split(","))
        {
            var ids      = idRange.Split("-");
            var firstId  = long.Parse(ids[0]);
            var secondId = long.Parse(ids[1]);

            for (long id = firstId; id <= secondId; id++)
            {
                if (!IsValidId(id))
                {
                    invalidIds.Add(id);
                }
            }
        }

        return invalidIds.Sum();
    }

    private bool IsValidId(long id)
    {
        var idString = id.ToString();

        for (int split = 1; split <= idString.Length / 2; split++)
        {
            if (idString.Length % split != 0)
            {
                continue;
            }

            var parts      = idString.SplitByLength(split);
            if (parts.Distinct().ToList().Count == 1)
            {
                return false;
            }
        }

        return true;
    }
}