using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._2;

public class Part1 : AoCPart
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
        if (idString.Length % 2 == 1)
        {
            return true;
        }

        var firstPart  = idString.Substring(0, idString.Length / 2);
        var secondPart = idString.Substring(idString.Length    / 2, idString.Length / 2);
        return firstPart != secondPart;
    }
}