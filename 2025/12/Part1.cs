using Advent_of_Code.Utility;

namespace Advent_of_Code._2025._12;

public class Part1 : AoCPart
{
    public override List<(string, string)> Tests => [
        ("""
         0:
         ###
         ##.
         ##.
         
         1:
         ###
         ##.
         .##
         
         2:
         .##
         ###
         ##.
         
         3:
         ##.
         ###
         ##.
         
         4:
         ###
         #..
         ###
         
         5:
         ###
         .#.
         ###
         
         4x4: 0 0 0 0 2 0
         12x5: 1 0 1 0 2 2
         12x5: 1 0 1 0 3 2
         """, "2"),
    ];
    
    public static List<Shape>  Shapes       = [];
    public override object Run(string input)
    {
        Shapes.Clear();
        List<bool[]> currentShape = [];
        List<Space>  spaces       = [];
        foreach (var line in SplitInput(input, false))
        {
            if (line == "")
            {
                if (currentShape.Count == 0) 
                {
                    continue;
                }
                Shapes.Add(new Shape(currentShape));
                currentShape.Clear();
                continue;
            }

            if (line.EndsWith(':'))
            {
                continue;
            }

            if (line.StartsWith('.') || line.StartsWith('#'))
            {
                currentShape.Add(line.Select(c => c is '#').ToArray());
                continue;
            }

            var split = line.Split(" ");
            spaces.Add(new Space
            {
                DimensionX = int.Parse(
                    split[0][..^1]
                        .Split("x")[0]
                ),
                DimensionY = int.Parse(
                    split[0][..^1]
                        .Split("x")[1]
                ),
                ShapeList = split[1..]
                            .Select(int.Parse)
                            .ToArray()
            });
        }

        var fitting = 0;
        foreach (var space in spaces)
        {
            fitting += space.CanFit() ? 1 : 0;
        }
        
        return fitting;
    }
}