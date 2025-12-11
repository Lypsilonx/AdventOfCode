using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._11;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         aaa: you hhh
         you: bbb ccc
         bbb: ddd eee
         ccc: ddd eee fff
         ddd: ggg
         eee: out
         fff: out
         ggg: out
         hhh: ccc fff iii
         iii: out
         """, "5"),
    ];
    
    public override object Run(string input)
    {
        Dictionary<string, List<string>> connections = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(": ");
            var splitConnections = split[1]
                .Split(" ");
            connections[split[0]] = splitConnections.ToList();
        }

        var          splitCount = 0;
        List<string> paths      = ["you"];
        while (paths.Count > 0)
        {
            var from           = paths[0];
            var newConnections = connections[from];
            paths.RemoveAt(0);
            foreach (var nc in newConnections)
            {
                Console.WriteLine($"{from} -> {nc}");
                if (nc != "out")
                {
                    paths.Add(nc);
                }
                else
                {
                    splitCount++;
                }
            }
        }
        
        return splitCount;
    }
}