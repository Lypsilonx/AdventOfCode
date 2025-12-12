namespace Advent_of_Code._2025._12;

public struct Space
{
    public int           DimensionX;
    public int   DimensionY;
    public int[] ShapeList;

    public bool CanFit()
    {
        var totalVolume = 0;
        for (var index = 0; index < ShapeList.Length; index++)
        {
            var shape = ShapeList[index];
            totalVolume += shape * Part1.Shapes[index].Volume;
        }

        if (totalVolume > DimensionX * DimensionY)
        {
            return false;
        }

        return true;
    }
}