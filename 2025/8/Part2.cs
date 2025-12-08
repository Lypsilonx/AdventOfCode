using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._8;

public class Part2 : AoCPart
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
         """, "25272"),
    ];
    
    public override object Run(string input)
    {
        var           lines    = SplitInput(input);
        var           length   = lines.Length;
        List<Vector3> breakers = [];
        for (var index = 0; index < length; index++)
        {
            var line = lines[index];
            var split = line.Split(",")
                            .Select(int.Parse)
                            .ToList();
            var vec = new Vector3(split[0], split[1], split[2]);
            breakers.Add(vec);
        }

        var                       distances = new float[length, length];

        for (var x = 0; x < length; x++)
        {
            for (var y = 0; y < length; y++)
            {
                distances[x, y] = Vector3.Distance(breakers[x], breakers[y]);
            }
        }
        
        var circuits = new int[length];
        for (var i = 0; i < length; i++)
        {
            circuits[i] = i;
        }

        (float distance, int indexA, int indexB) lastConnection = (0, 0, 0);

        for (var circuitCount = length; circuitCount > 1; circuitCount--)
        {
            var closestDistance = FindClosest();

            var circuitA = circuits[closestDistance.indexA];
            var circuitB = circuits[closestDistance.indexB];

            lastConnection = closestDistance;

            for (var j = 0; j < length; j++)
            {
                if (circuits[j] == circuitA)
                {
                    circuits[j] = circuitB;
                }
            }
        }

        return (long) breakers[lastConnection.indexA].X * (long) breakers[lastConnection.indexB].X;

        (float distance, int indexA, int indexB) FindClosest()
        {
            (float distance, int indexA, int indexB) closestDistance = (float.MaxValue, 0, 0);
            for (var x = 0; x < length; x++)
            {
                for (var y = 0; y < length; y++)
                {
                    var distance = distances[x, y];

                    if (circuits[x] == circuits[y])
                    {
                        continue;
                    }

                    if (distance < closestDistance.distance)
                    {
                        closestDistance = (distance, x, y);
                    }
                }
            }

            return closestDistance;
        }
    }
}