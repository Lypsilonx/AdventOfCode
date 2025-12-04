using System.Numerics;

namespace Advent_of_Code.Utility;

public static class Extensions
{
    public static Vector3 Select(this Vector3 self, Func<float, float> function)
    {
        return new Vector3(function(self.X), function(self.Y), function(self.Z));
    }

    public static IEnumerable<string> SplitByLength(this string str, int maxLength)
    {
        for (var index = 0; index < str.Length; index += maxLength)
        {
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

    public static string Join<T>(this List<T> list, string join)
    {
        return string.Join(join, list);
    }

    public static int ToInt(this List<bool> bits)
    {
        var num = 0;

        for (var i = 0; i < bits.Count; i++)
        {
            num += (bits[i]
                        ? 1
                        : 0)
                   << (bits.Count - i - 1);
        }

        return num;
    }
}