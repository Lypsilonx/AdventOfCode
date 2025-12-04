using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Advent_of_Code.Utility;

public static class Runner
{
    [field: AllowNull]
    [field: MaybeNull]
    public static string ProjectDirectory
    {
        get
        {
            field ??= Environment.CurrentDirectory.Contains("/bin")
                          ? Directory.GetParent(Environment.CurrentDirectory)
                                     ?.Parent?.Parent?.FullName
                            ?? string.Empty
                          : Environment.CurrentDirectory;
            return field;
        }
    }

    private static bool CreateFilesForToday(int pYear, int pDay)
    {
        var filesCreated = false;

        if (!Directory.Exists($"{ProjectDirectory}/{pYear}"))
        {
            Directory.CreateDirectory($"{ProjectDirectory}/{pYear}");
            Console.WriteLine($"Creating directory for {pYear}");
            filesCreated = true;
        }

        if (!Directory.Exists($"{ProjectDirectory}/{pYear}/{pDay}"))
        {
            Directory.CreateDirectory($"{ProjectDirectory}/{pYear}/{pDay}");
            Console.WriteLine($"Creating directory for {pYear}/{pDay}");
            filesCreated = true;
        }

        if (!File.Exists($"{ProjectDirectory}/{pYear}/{pDay}/Part1.cs"))
        {
            File.WriteAllText(
                $"{ProjectDirectory}/{pYear}/{pDay}/Part1.cs",
                $$"""
                  using Advent_of_Code.Utility;

                  namespace Advent_of_Code._{{pYear}}._{{pDay}};

                  public class Part1 : {{nameof(AoCPart)}}
                  {
                      public override object Run()
                      {
                          return "";
                      }
                  }
                  """
            );
            Console.WriteLine($"Creating Part1 for {pYear}/{pDay}");
            filesCreated = true;
        }

        if (!File.Exists($"{ProjectDirectory}/{pYear}/{pDay}/Part2.cs"))
        {
            File.WriteAllText(
                $"{ProjectDirectory}/{pYear}/{pDay}/Part2.cs",
                $$"""
                  using Advent_of_Code.Utility;

                  namespace Advent_of_Code._{{pYear}}._{{pDay}};

                  public class Part2 : {{nameof(AoCPart)}}
                  {
                      public override object Run()
                      {
                          return "";
                      }
                  }
                  """
            );
            Console.WriteLine($"Creating Part2 for {pYear}/{pDay}");
            filesCreated = true;
        }

        return filesCreated;
    }

    public static void Run(int pYear, int pDay, int pPart = 0, bool pSubmit = false)
    {
        if (CreateFilesForToday(pYear, pDay))
        {
            return;
        }

        if (pPart == 0)
        {
            pPart = File.ReadAllText($"{ProjectDirectory}/{pYear}/{pDay}/Part2.cs")
                        .Contains("{\n        return \"\";\n    }")
                        ? 1
                        : 2;
        }

        var type = Assembly.Load("Advent of Code, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                           .GetType($"Advent_of_Code._{pYear}._{pDay}.Part{pPart}");

        if (type == null)
        {
            throw new ArgumentOutOfRangeException($"Could not find {pYear}/{pDay}/{pPart}");
        }

        var partObject = (Activator.CreateInstance(type) as AoCPart)!;

        var watch     = Stopwatch.StartNew();
        var outputRaw = partObject.Run();
        watch.Stop();
        Console.WriteLine(
            $"{pYear}/{pDay}/{pPart}: "
            + $"({(float) (watch.ElapsedTicks - AoCPart.InputWatch.ElapsedTicks) / Stopwatch.Frequency * 1000}ms)"
        );

        var output = outputRaw.ToString();
        Console.WriteLine(output);

        var solved = File.ReadAllLines($"{ProjectDirectory}/Utility/solved.txt");
        if (!pSubmit || string.IsNullOrEmpty(output) || solved.Contains($"{pYear}/{pDay}/{pPart}"))
        {
            return;
        }

        Console.WriteLine("Submit [Y/n]");
        var submitAnswer = Console.ReadLine();

        if (submitAnswer == "n")
        {
            return;
        }

        partObject.Submit(output);
    }
}