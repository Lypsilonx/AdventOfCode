using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._11;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         svr: aaa bbb
         aaa: fft
         fft: ccc
         bbb: tty
         tty: ccc
         ccc: ddd eee
         ddd: hub
         hub: fff
         eee: dac
         dac: fff
         fff: ggg hhh
         ggg: out
         hhh: out
         """, "2"),
    ];

    public override object Run(string input)
    {
        Dictionary<string, List<string>> connectionsBck = [];
        Dictionary<string, List<string>> connectionsFwd = [];
        foreach (var line in SplitInput(input))
        {
            var split = line.Split(": ");
            var splitConnections = split[1]
                .Split(" ");

            foreach (var connection in splitConnections)
            {
                if (connectionsBck.ContainsKey(connection))
                {
                    connectionsBck[connection].Add(split[0]);
                }
                else
                {
                    connectionsBck[connection] = [split[0]];
                }
            }
            connectionsFwd[split[0]] = splitConnections.ToList();
        }

        long splitCount  = 0;
        var fftPaths    = GetFromTo(connectionsBck,   "fft", "svr");
        var dacOutPaths = GetFromTo(connectionsFwd,   "dac", "out");
        var fftDacPaths = GetFromTo(connectionsBck, "dac", "fft");

        splitCount += fftDacPaths * fftPaths * dacOutPaths;

        return splitCount;

        long GetFromTo(Dictionary<string, List<string>> connections, string getFrom, string getTo)
        {
            List<(string, long)> paths     = [(getFrom, 1)];
            long goalPaths = 0;

            while (paths.Count > 0)
            {
                List<(string, long)> newPaths = [];
                foreach (var path in paths)
                {
                    if (path.Item1 == getTo)
                    {
                        goalPaths += path.Item2;
                    }
                    
                    if (path.Item1 is "svr" or "out")
                    {
                        continue;
                    }

                    newPaths.AddRange(connections[path.Item1].Select(c => (c, path.Item2)));
                }

                paths.Clear();
                foreach (var path in newPaths.Select(p => p.Item1).Distinct())
                {
                    paths.Add((path, newPaths.Where(p => p.Item1 == path).Sum(p => p.Item2)));
                }
            }

            return goalPaths;
        }
    }
}