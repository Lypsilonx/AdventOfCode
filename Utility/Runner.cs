using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Advent_of_Code.Utility;

public static class Runner
{
    [field: AllowNull]
    [field: MaybeNull]
    private static string _projectDirectory
    {
        get
        {
            field ??= AppDomain.CurrentDomain.BaseDirectory.Split("/Advent of Code")[0] + "/Advent of Code";
            return field;
        }
    }

    private static bool CreateFilesForToday(int pYear, int pDay)
    {
        var filesCreated = false;

        if (!Directory.Exists($"{_projectDirectory}/{pYear}"))
        {
            Directory.CreateDirectory($"{_projectDirectory}/{pYear}");
            Console.WriteLine($"Creating directory for {pYear}");
            filesCreated = true;
        }

        if (!Directory.Exists($"{_projectDirectory}/{pYear}/{pDay}"))
        {
            Directory.CreateDirectory($"{_projectDirectory}/{pYear}/{pDay}");
            Console.WriteLine($"Creating directory for {pYear}/{pDay}");
            filesCreated = true;
        }

        if (!File.Exists($"{_projectDirectory}/{pYear}/{pDay}/Part1.cs"))
        {
            File.WriteAllText(
                $"{_projectDirectory}/{pYear}/{pDay}/Part1.cs",
                $$"""""""
                  using Advent_of_Code.Utility;

                  namespace Advent_of_Code._{{pYear}}._{{pDay}};

                  public class Part1 : {{nameof(AoCPart)}}
                  {
                      public override List<(string, string)> Tests => [
                          ("""
                           input
                           """, ""),
                      ];
                      
                      public override object Run(string input)
                      {
                          return "";
                      }
                  }
                  """""""
            );
            Console.WriteLine($"Creating Part1 for {pYear}/{pDay}");
            filesCreated = true;
        }

        if (!File.Exists($"{_projectDirectory}/{pYear}/{pDay}/Part2.cs"))
        {
            File.WriteAllText(
                $"{_projectDirectory}/{pYear}/{pDay}/Part2.cs",
                $$"""""""
                  using Advent_of_Code.Utility;

                  namespace Advent_of_Code._{{pYear}}._{{pDay}};

                  public class Part2 : {{nameof(AoCPart)}}
                  {
                      public override List<(string, string)> Tests => [
                          ("""
                           input
                           """, ""),
                      ];
                      
                      public override object Run(string input)
                      {
                          return "";
                      }
                  }
                  """""""
            );
            Console.WriteLine($"Creating Part2 for {pYear}/{pDay}");
            filesCreated = true;
        }

        return filesCreated;
    }

    private static string Input(int pYear, int pDay)
    {
        var filePath = $"{_projectDirectory}/{pYear}/{pDay}/input.txt";

        if (File.Exists(filePath))
        {
            var fileContents = File.ReadAllText(filePath);

            return fileContents;
        }

        var       baseAddress     = new Uri("https://adventofcode.com");
        var       cookieContainer = new CookieContainer();
        using var client          = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer });
        client.BaseAddress = baseAddress;
        cookieContainer.Add(
            baseAddress,
            new Cookie("session", File.ReadAllText($"{_projectDirectory}/Utility/token.txt"))
        );
        var result = client.GetAsync($"{pYear}/day/{pDay}/input")
                           .Result;
        result.EnsureSuccessStatusCode();

        var text = result.Content.ReadAsStringAsync()
                         .Result;

        File.WriteAllText(filePath, text);

        return text;
    }

    public static void Run(int pYear, int pDay, int pPart = 0, bool pSubmit = false)
    {
        if (CreateFilesForToday(pYear, pDay))
        {
            return;
        }

        if (pPart == 0)
        {
            pPart = File.ReadAllText($"{_projectDirectory}/{pYear}/{pDay}/Part2.cs")
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

        if (partObject.Tests.Count != 0)
        {
            var multiTest = partObject.Tests.Count > 1;
            for (var index = 0; index < partObject.Tests.Count; index++)
            {
                var indexString = multiTest
                                      ? $" {index}"
                                      : "";
                var test = partObject.Tests[index];
                var testOutput = partObject.Run(test.Input)
                                           .ToString();

                if (testOutput != test.Solution)
                {
                    Console.WriteLine($"Test{indexString} failed.");
                    Console.WriteLine($"Got \"{testOutput}\", expected \"{test.Solution}\".");
                    return;
                }

                Console.WriteLine($"Test{indexString} passed.");
            }

            if (multiTest)
            {
                Console.WriteLine("All tests passed.");
            }
        }

        var input     = Input(pYear, pDay);
        var watch     = Stopwatch.StartNew();
        var outputRaw = partObject.Run(input);
        watch.Stop();
        Console.WriteLine($"{pYear}/{pDay}/{pPart}: ({(float) watch.ElapsedTicks / Stopwatch.Frequency * 1000}ms)");

        var output = outputRaw.ToString();
        Console.WriteLine(output);

        var solved = File.ReadAllLines($"{_projectDirectory}/Utility/solved.txt");
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

        Submit(pYear, pDay, pPart, output);
    }

    private static void Submit(int pYear, int pDay, int pPart, string answer)
    {
        var       baseAddress     = new Uri("https://adventofcode.com");
        var       cookieContainer = new CookieContainer();
        using var client          = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer });
        client.BaseAddress = baseAddress;
        cookieContainer.Add(
            baseAddress,
            new Cookie("session", File.ReadAllText($"{_projectDirectory}/Utility/token.txt"))
        );
        var formContent = new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>> { new("level", pPart.ToString()), new("answer", answer) }
        );
        var result = client.PostAsync($"{pYear}/day/{pDay}/answer", formContent)
                           .Result;

        result.EnsureSuccessStatusCode();

        var htmlContent = result.Content.ReadAsStringAsync()
                                .Result;
        htmlContent = Regex.Replace(htmlContent, "<a \n?.*?>(.*?)</a\n?>",       "${1}", RegexOptions.Singleline);
        htmlContent = Regex.Replace(htmlContent, "<span \n?.*?>(.*?)</span\n?>", "${1}", RegexOptions.Singleline);

        var matches = Regex.Matches(htmlContent, "<article><p>(.*?)</p>", RegexOptions.Singleline);

        var response = matches.First()
                              .Groups[1]
                              .Value.Replace("</p><p>", "\n");

        if (response.Contains("That's the right answer") || response.Contains("complete"))
        {
            File.AppendAllLines($"{_projectDirectory}/Utility/solved.txt", [$"{pYear}/{pDay}/{pPart}"]);
        }

        Console.WriteLine(response);
    }
}