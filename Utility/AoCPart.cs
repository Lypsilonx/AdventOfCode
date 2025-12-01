using System.Net;
using System.Text.RegularExpressions;

namespace Advent_of_Code.Utility;

public abstract class AoCPart
{
    protected string Input {
        get
        {
            var filePath        = $"../../../{_year}/{_day}/input.txt";

            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            
            var baseAddress     = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            using var client          = new HttpClient(new HttpClientHandler
            {
                CookieContainer = cookieContainer
            });
            client.BaseAddress = baseAddress;
            cookieContainer.Add(baseAddress, new Cookie("session", File.ReadAllText("../../../Utility/token.txt")));
            var result = client.GetAsync($"{_year}/day/{_day}/input").Result;
            result.EnsureSuccessStatusCode();

            var text = result.Content.ReadAsStringAsync()
                             .Result;
            
            File.WriteAllText(filePath, text);
            
            return text;
        }
    }

    protected string[] InputLines(bool removeEmpty = true)
    {
        var lines = Input.Split("\n");
        if (removeEmpty)
        {
            lines = lines.Where(l => l != "").ToArray();
        }
        return lines;
    }

    private int _year => int.Parse(GetType().Namespace!.Split(".")[1][1..]);
    private int _day  => int.Parse(GetType().Namespace!.Split(".")[2][1..]);
    private int _part =>
        int.Parse(
            GetType().Name.Last()
                     .ToString()
        );

    public abstract object Run();
    
    public void Submit(string answer)
    {
        var baseAddress     = new Uri("https://adventofcode.com");
        var cookieContainer = new CookieContainer();
        using var client          = new HttpClient(new HttpClientHandler
        {
            CookieContainer = cookieContainer
        });
        client.BaseAddress = baseAddress;
        cookieContainer.Add(baseAddress, new Cookie("session", File.ReadAllText("../../../Utility/token.txt")));
        var formContent   = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new ("level", _part.ToString()),
            new ("answer", answer)
        });
        var result        = client.PostAsync($"{_year}/day/{_day}/answer", formContent).Result;
    
        result.EnsureSuccessStatusCode();

        var htmlContent = result.Content.ReadAsStringAsync()
                                .Result;
        MatchCollection matches     = Regex.Matches(htmlContent, "<article><p>(.*?)</p></article>");

        var response = matches.First()
                              .Groups[1].Value;
        
        if (response.Contains("solved") || response.Contains("complete"))
        {
            File.AppendAllLines("../../../Utility/solved.txt",
            [
                $"{_year}/{_day}/{_part}"
            ]
            );
        }

        Console.WriteLine(response);
    }
}