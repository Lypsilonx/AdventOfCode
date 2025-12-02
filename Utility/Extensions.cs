using System.Numerics;

namespace Advent_of_Code.Utility;

public static class Extensions
{
    public static Vector3 Select(this Vector3 self, Func<float, float> function)
    {
        return new Vector3(
            function(self.X),
            function(self.Y),
            function(self.Z)
        );
    }
    
    public static IEnumerable<string> SplitByLength(this string str, int maxLength) {
        for (int index = 0; index < str.Length; index += maxLength) {
            yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
        }
    }
    
    public static Vector3 Round(this Vector3 self)
    {
        return self.Select(x => (float) Math.Round(x));
    }
    
    public static float Manhattan(this Vector3 self, Vector3 other)
    {
        return Math.Abs(self.X - other.X) + Math.Abs(self.Y - other.Y) + Math.Abs(self.Z - other.Z);
    }
}