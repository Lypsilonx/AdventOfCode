using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._8;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         162,817,812
         57,618,57
         906,360,560
         592,479,940
         352,342,300
         466,668,158
         542,29,236
         431,825,988
         739,650,466
         52,470,668
         216,146,977
         819,987,18
         117,168,530
         805,96,715
         346,949,466
         970,615,88
         941,993,340
         862,61,35
         984,92,344
         425,690,689
         """, "40"),
    ];
    
    public override object Run(string input)
    {
        var           lines     = SplitInput(input);
        var           toConnect = lines.Length == 20 ? 10 : 1000;
        List<Vector3> breakers  = [];
        foreach (var line in lines)
        {
            var split = line.Split(",").Select(int.Parse).ToList();
            var vec   = new Vector3(split[0], split[1], split[2]);
            breakers.Add(vec);
        }

        float[,]                       distances = new float[breakers.Count, breakers.Count];

        for (int x = 0; x < breakers.Count; x++)
        {
            for (int y = 0; y < breakers.Count; y++)
            {
                if (x == y)
                {
                    distances[x, y] = float.MaxValue;
                    continue;
                }
                distances[x, y] = Vector3.Distance(breakers[x], breakers[y]);
            }
        }
        
        List<List<int>> circuits = breakers.Enumerate().Select(x => new List<int>([x.Index])).ToList();
        
        (float distance, int indexA, int indexB) FindClosest()
        {
            (float distance, int indexA, int indexB) closestDistance = (float.MaxValue, 0, 0);
            for (int i = 0; i < distances.Length; i++)
            {
                var x        = i % breakers.Count;
                var y        = i / breakers.Count;
                var distance = distances[x, y];
                
                if (distance < closestDistance.distance)
                {
                    closestDistance = (distance, x, y);
                }
            }
            
            return closestDistance;
        }

        for (int i = 0; i < toConnect; i++)
        {
            (float distance, int indexA, int indexB) closestDistance = FindClosest();

            var circuitA = circuits.First(c => c.Contains(closestDistance.indexA));
            var circuitB = circuits.First(c => c.Contains(closestDistance.indexB));
            distances[closestDistance.indexA, closestDistance.indexB] = float.MaxValue;
            distances[closestDistance.indexB, closestDistance.indexA] = float.MaxValue;

            if (circuitA == circuitB)
            {
                continue;
            } 
            
            circuits.Remove(circuitB);
            circuitA.AddRange(circuitB);
        }

        var largest = circuits.Select(x => x.Count)
                              .OrderByDescending(x => x)
                              .Take(3)
                              .ToList();
        return largest.Mult();
    }
}