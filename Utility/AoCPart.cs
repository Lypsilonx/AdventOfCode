using System.Net;

namespace Advent_of_Code.Utility;

public abstract class AoCPart
{
    public string Input {
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
            })
            {
                BaseAddress = baseAddress
            };
            cookieContainer.Add(baseAddress, new Cookie("session", File.ReadAllText("../../../Utility/token.txt")));
            var result = client.GetAsync($"{_year}/day/{_day}/input").Result;
            result.EnsureSuccessStatusCode();

            var text = result.Content.ReadAsStringAsync()
                             .Result;
            
            File.WriteAllText(filePath, text);
            
            return text;
        }
    }
    
    public string[] InputLines(bool removeEmpty = true)
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
    private int _part => int.Parse(GetType().Namespace!.Last().ToString());

    public abstract object Run();
}