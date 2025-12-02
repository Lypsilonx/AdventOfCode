using System.Reflection;

namespace Advent_of_Code.Utility;

public static class Runner
{
    private static bool CreateFilesForToday(int pYear, int pDay)
    {
        var filesCreated   = false;

        if (!Directory.Exists($"../../../{pYear}"))
        {
            Directory.CreateDirectory($"../../../{pYear}");
            Console.WriteLine($"Creating directory for {pYear}");
            filesCreated = true;
        }
        
        if (!Directory.Exists($"../../../{pYear}/{pDay}"))
        {
            Directory.CreateDirectory($"../../../{pYear}/{pDay}");
            Console.WriteLine($"Creating directory for {pYear}/{pDay}");
            filesCreated = true;
        }

        if (!File.Exists($"../../../{pYear}/{pDay}/Part1.cs"))
        {
            File.WriteAllText(
                $"../../../{pYear}/{pDay}/Part1.cs",
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
                  """);
            Console.WriteLine($"Creating Part1 for {pYear}/{pDay}");
            filesCreated = true;
        }
        
        if (!File.Exists($"../../../{pYear}/{pDay}/Part2.cs"))
        {
            File.WriteAllText(
                $"../../../{pYear}/{pDay}/Part2.cs",
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
                  """);
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
            pPart = File.ReadAllText($"../../../{pYear}/{pDay}/Part2.cs").Contains("{\n        return \"\";\n    }")
                ? 1
                : 2;
        }

        var type = Assembly.Load("Advent of Code, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                           .GetType($"Advent_of_Code._{pYear}._{pDay}.Part{pPart}");

        if (type == null)
        {
            throw new ArgumentOutOfRangeException($"Could not find {pYear}/{pDay}/{pPart}");
        }
     
        AoCPart partObject = (Activator.CreateInstance(type) as AoCPart)!;
        
        Console.WriteLine($"{pYear}/{pDay}/{pPart}:");

        var output = partObject.Run().ToString();
        
        Console.WriteLine(output);

        var solved = File.ReadAllLines("../../../Utility/solved.txt");
        if (!pSubmit
            || string.IsNullOrEmpty(output)
            || solved.Contains($"{pYear}/{pDay}/{pPart}"))
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