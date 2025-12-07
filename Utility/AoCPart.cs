namespace Advent_of_Code.Utility;

public abstract class AoCPart
{
    public virtual List<(string Input, string Solution)> Tests => [];

    protected string[] SplitInput(string input, bool removeEmpty = true)
    {
        var lines = input.Split("\n");
        if (removeEmpty)
        {
            lines = lines.Where(l => l != "")
                         .ToArray();
        }

        return lines;
    }

    public abstract object Run(string input);
}