using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._2;

public class Part1 : AoCPart
{
    public override string TestInput =>
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124";

    public override string TestSolution => "1227775554";

    public override object Run()
    {
        List<long> invalidIds = [];
        foreach (var idRange in Input.Split(","))
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
        if (idString.Length % 2 == 1)
        {
            return true;
        }

        var firstPart  = idString[..(idString.Length / 2)];
        var secondPart = idString.Substring(idString.Length / 2, idString.Length / 2);
        return firstPart != secondPart;
    }
}