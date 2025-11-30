using System.Numerics;
using Advent_of_Code.Utility;

namespace Advent_of_Code._1;

public class Scanner
{
    public        List<Vector3> Beacons;
    private const float         RAD_90 = (float) Math.PI / 2f;

    private Dictionary<Quaternion, List<Vector3>> Rotations()
    {
        List<Quaternion> rotationQuaternions = [
            Quaternion.CreateFromYawPitchRoll(0,  0,  0),
            Quaternion.CreateFromYawPitchRoll(0,  0,  RAD_90),
            Quaternion.CreateFromYawPitchRoll(0,  0,  -RAD_90),
            Quaternion.CreateFromYawPitchRoll(0,  0,  2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(0, RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(0, RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(0, RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(0, RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(0, -RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(0, -RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(0, -RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(0, -RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(0,     2*RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(0,     2*RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(0,     2*RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(0,     2*RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(RAD_90, 0,   0),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 0,   RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 0,   -RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 0,   2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(RAD_90, RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(RAD_90, RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(RAD_90, -RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(RAD_90, -RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, -RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, -RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(RAD_90, 2*RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 2*RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 2*RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(RAD_90, 2*RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 0, 0),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 0, RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 0, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 0, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(-RAD_90, RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(-RAD_90, -RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, -RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, -RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, -RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 2*RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 2*RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 2*RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(-RAD_90, 2*RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 0, 0),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 0, RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 0, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 0, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, -RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, -RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, -RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, -RAD_90, 2*RAD_90),
            
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 2*RAD_90, 0),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 2*RAD_90, RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 2*RAD_90, -RAD_90),
            Quaternion.CreateFromYawPitchRoll(2*RAD_90, 2*RAD_90, 2*RAD_90)
        ];
        rotationQuaternions = rotationQuaternions.Distinct().ToList();

        Dictionary<Quaternion, List<Vector3>> rotations = new();
        foreach (var quaternion in rotationQuaternions)
        {
            var rotatedBeacons = Beacons.Select(v => Vector3.Transform(v, quaternion).Round()).ToList();
            rotations.Add(quaternion, rotatedBeacons);
        }

        return rotations;
    }

    public (Quaternion, Vector3)? CompareTo(Scanner other)
    {
        foreach (var (quaternion, otherRotation) in other.Rotations())
        {
            Dictionary<Vector3, int> distances = new();
            foreach (var ownBeacon in Beacons)
            {
                foreach (var otherBeacon in otherRotation)
                {
                    var distance = ownBeacon - otherBeacon;
                    if (!distances.TryAdd(distance, 1))
                    {
                        distances[distance]++;

                        if (distances[distance] >= 12)
                        {
                            return (quaternion, distance);
                        }
                    }
                }
            }
        }

        return null;
    }

    public void MergeWith(Scanner other, Quaternion quaternion, Vector3 distance)
    {
        var otherBeacons = other.Beacons.Select(v => Vector3.Transform(v, quaternion).Round() + distance).ToList();

        var matches = 0;
        foreach (var otherBeacon in otherBeacons)
        {
            if (!Beacons.Contains(otherBeacon))
            {
                Beacons.Add(otherBeacon);
            }
            else
            {
                matches++;
            }
        }
    }
}