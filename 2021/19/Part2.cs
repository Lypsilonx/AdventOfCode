using System.Numerics;
using Advent_of_Code.Utility;
using Advent_of_Code._1;

namespace Advent_of_Code._2021._19;

public class Part2 : AoCPart
{
    public override object Run()
    {
        List<Scanner> scanners = new();
        
        Scanner? scanner = null;
        foreach (var line in InputLines())
        {
            if (line.StartsWith("---") )
            {
                if (scanner != null) { 
                    scanners.Add(scanner);
                }

                scanner         = new Scanner();
                scanner.Beacons = new();
            }
            else
            {
                var values = line.Split(",");
                scanner!.Beacons.Add(
                    new Vector3(
                        int.Parse(values[0]),
                        int.Parse(values[1]),
                        int.Parse(values[2])
                    )
                );
            }
        }
        scanners.Add(scanner!);

        var rootScanner = scanners[0];
        scanners.RemoveAt(0);

        List<Vector3> scannerPositions = [
            Vector3.Zero
        ];
        while (scanners.Count > 0)
        {
            foreach (var other in scanners)
            {
                var comparisonResult = rootScanner.CompareTo(other);
                if (comparisonResult != null)
                {
                    rootScanner.MergeWith(other, comparisonResult.Value.Item1, comparisonResult.Value.Item2);
                    scanners.Remove(other);
                    scannerPositions.Add(comparisonResult.Value.Item2);

                    break;
                }
            }
        }

        float greatestDistance = 0;
        foreach (var posA in scannerPositions)
        {
            foreach (var posB in scannerPositions)
            {
                var manhattanDistance = posA.Manhattan(posB);
                if (manhattanDistance > greatestDistance)
                {
                    greatestDistance = manhattanDistance;
                }
            }
        }

        return greatestDistance;
    }
}