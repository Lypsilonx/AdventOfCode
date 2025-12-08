using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._8;

public class Part2 : AoCPart
{
    public override List<(string, string)> Tests =>
    [
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
         """, "25272")
    ];

    public override object Run(string input)
    {
        var                            lines    = SplitInput(input);
        ushort                         length   = (ushort) lines.Length;
        List<(uint X, uint Y, uint Z)> breakers = [];
        for (ushort index = 0; index < length; index++)
        {
            var line = lines[index];
            var split = line.Split(",")
                            .Select(uint.Parse)
                            .ToList();
            breakers.Add((split[0], split[1], split[2]));
        }
        
        SortedSet<KeyValuePair<uint, (ushort, ushort)>> distances = new(new KeyValueComparer<uint, (ushort, ushort)>());

        for (ushort x = 0; x < length; x++)
        {
            for (ushort y = 0; y < length; y++)
            {
                if (x >= y)
                {
                    continue;
                }
                distances.Add(new KeyValuePair<uint, (ushort, ushort)>(LazyDistance(breakers[x], breakers[y]), (x, y)));
            }
        }

        var circuits = new ushort[length];
        for (ushort i = 0; i < length; i++)
        {
            circuits[i] = i;
        }

        (ushort indexA, ushort indexB) lastConnection = (0, 0);

        for (var circuitCount = length; circuitCount > 1; circuitCount--)
        {
            var closestDistance = FindClosest();

            var circuitA = circuits[closestDistance.indexA];
            var circuitB = circuits[closestDistance.indexB];

            lastConnection = closestDistance;

            for (ushort j = 0; j < length; j++)
            {
                if (circuits[j] == circuitA)
                {
                    circuits[j] = circuitB;
                }
            }
        }

        return breakers[lastConnection.indexA].X * breakers[lastConnection.indexB].X;
        
        uint LazyDistance((uint X, uint Y, uint Z) self, (uint X, uint Y, uint Z) other)
        {
            var dX = self.X - other.X;
            var dY = self.Y - other.Y;
            var dZ = self.Z - other.Z;
            return dX * dX + dY * dY + dZ * dZ;
        }

        (ushort indexA, ushort indexB) FindClosest()
        {
            (ushort indexA, ushort indexB) closestDistance;
            do
            {
                var min = distances.Min;
                closestDistance = min.Value;
                distances.Remove(min);
            } while (circuits[closestDistance.indexA] == circuits[closestDistance.indexB]);

            return closestDistance;
        }
    }
        
    private class KeyValueComparer<K, V> : IComparer<KeyValuePair<K, V>>
        where K : IComparable
        where V : IComparable
    {
        public int Compare(KeyValuePair<K, V> x, KeyValuePair<K, V> y)
        {
            var res = x.Key.CompareTo(y.Key);
            return res == 0 ? x.Value.CompareTo(y.Value) : res;
        }
    }
}