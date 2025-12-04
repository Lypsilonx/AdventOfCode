using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Advent_of_Code.Utility;

public abstract class AoCPart
{
    public static readonly  Stopwatch InputWatch        = new();
    protected string Input
    {
        get
        {
            var onlyInput = false;
            if (!InputWatch.IsRunning)
            {
                onlyInput = true;
                InputWatch.Start();
            }
            var filePath = $"{Runner.ProjectDirectory}/{_year}/{_day}/input.txt";

            if (File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                if (onlyInput)
                {
                    InputWatch.Stop();
                }
                return fileContents;
            }

            var       baseAddress     = new Uri("https://adventofcode.com");
            var       cookieContainer = new CookieContainer();
            using var client          = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer });
            client.BaseAddress = baseAddress;
            cookieContainer.Add(baseAddress, new Cookie("session", File.ReadAllText($"{Runner.ProjectDirectory}/Utility/token.txt")));
            var result = client.GetAsync($"{_year}/day/{_day}/input")
                               .Result;
            result.EnsureSuccessStatusCode();

            var text = result.Content.ReadAsStringAsync()
                             .Result;

            File.WriteAllText(filePath, text);
            if (onlyInput)
            {
                InputWatch.Stop();
            }
            return text;
        }
    }

    private int _year =>
        int.Parse(
            GetType()
                .Namespace!.Split(".")[1][1..]
        );

    private int _day =>
        int.Parse(
            GetType()
                .Namespace!.Split(".")[2][1..]
        );

    private int _part =>
        int.Parse(
            GetType()
                .Name.Last()
                .ToString()
        );

    protected string[] InputLines(bool removeEmpty = true)
    {
        InputWatch.Start();
        var lines = Input.Split("\n");
        if (removeEmpty)
        {
            lines = lines.Where(l => l != "")
                         .ToArray();
        }
        InputWatch.Stop();

        return lines;
    }

    public abstract object Run();

    public void Submit(string answer)
    {
        var       baseAddress     = new Uri("https://adventofcode.com");
        var       cookieContainer = new CookieContainer();
        using var client          = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer });
        client.BaseAddress = baseAddress;
        cookieContainer.Add(baseAddress, new Cookie("session", File.ReadAllText($"{Runner.ProjectDirectory}/Utility/token.txt")));
        var formContent = new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>> { new("level", _part.ToString()), new("answer", answer) }
        );
        var result = client.PostAsync($"{_year}/day/{_day}/answer", formContent)
                           .Result;
        
        result.EnsureSuccessStatusCode();
        
        var htmlContent = result.Content.ReadAsStringAsync()
                                .Result;
        htmlContent = Regex.Replace(htmlContent, "<a \n?.*?>(.*?)</a\n?>",       "${1}", RegexOptions.Singleline);
        htmlContent = Regex.Replace(htmlContent, "<span \n?.*?>(.*?)</span\n?>", "${1}", RegexOptions.Singleline);
            
        var matches     = Regex.Matches(htmlContent, "<article><p>(.*?)</p>", RegexOptions.Singleline);

        var response = matches.First()
                              .Groups[1]
                              .Value.Replace("</p><p>", "\n");

        if (response.Contains("That's the right answer") || response.Contains("complete"))
        {
            File.AppendAllLines($"{Runner.ProjectDirectory}/Utility/solved.txt", [$"{_year}/{_day}/{_part}"]);
        }

        Console.WriteLine(response);
    }
}