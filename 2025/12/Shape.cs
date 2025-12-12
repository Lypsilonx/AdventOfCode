namespace Advent_of_Code._2025._12;

public class Shape
{
    private bool[,]       Occupies;
    private List<bool[,]> Variations => [
        Occupies,
        Rotate(Occupies, 1),
        Rotate(Occupies, -1),
        Rotate(Occupies, 2),
        Flip(Occupies),
        Rotate(Flip(Occupies), 1),
        Rotate(Flip(Occupies), -1),
        Rotate(Flip(Occupies), 2)
    ];

    public int Volume {
        get {
            if (field != 0)
            {
                return field;
            }
            
            var v = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    v += Occupies[x, y] ? 1 : 0;
                }
            }

            field = v;
            
            return v;
        }
    } = 0;

    public Shape(List<bool[]> input)
    {
        Occupies = new bool[3, 3];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Occupies[x, y] = input[y][x];
            }
        }
    }

    private static bool[,] Rotate(bool[,] shape, int index)
    {
        var outShape = new bool[3, 3];
        switch (index)
        {
            case 1:
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        outShape[x, y] = shape[2 - y, x];
                    }
                }
                break;
            case -1:
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        outShape[x, y] = shape[y, 2 - x];
                    }
                }
                break;
            case 2:
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        outShape[x, y] = shape[2 - x, 2 - y];
                    }
                }
                break;
        }
        
        return outShape;
    }
    
    private static bool[,] Flip(bool[,] shape)
    {
        var outShape = new bool[3, 3];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                outShape[x, y] = shape[2 - x, y];
            }
        }
        
        return outShape;
    }
}