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

    public static bool TryGet<T>(this List<T>? list, int index, out T value)
    {
        if (list != null && index >= 0 && index < list.Count)
        {
            value = list[index];
            return true;
        }

        value = default!;
        return false;
    }

    public static bool TryGet<T>(this List<T> list, Func<T, bool> filter, out List<T> values)
    {
        values = list.Where(filter)
                     .ToList();
        return values.Count > 0;
    }

    public static bool TryGetFirst<T>(this List<T> list, Func<T, bool> filter, out T? value)
    {
        foreach (var element in list)
        {
            if (!filter(element))
            {
                continue;
            }

            value = element;
            return true;
        }

        value = default;
        return false;
    }

    public static T Mult<T>(this List<T> list) where T : INumber<T>
    {
        return list.Aggregate((a, i) => a * i);
    }

    public static string ReplaceRecursive(this string input, string pattern, string replace)
    {
        while (input.Contains(pattern))
        {
            input = input.Replace(pattern, replace);
        }

        return input;
    }

    public static void ForceAdd<TKey, TNumber>(this Dictionary<TKey, TNumber> dictionary, TKey key, TNumber amount)
        where TNumber : INumber<TNumber> where TKey : notnull
    {
        dictionary[key] = dictionary.TryGetValue(key, out var prev)
                              ? prev + amount
                              : amount;
    }

    public static IEnumerable<(T Value, int Index)> Enumerate<T>(this IEnumerable<T> list)
    {
        var index = 0;
        return list.Select(x =>
            {
                var i = index++;
                return (x, i);
            }
        );
    }
}