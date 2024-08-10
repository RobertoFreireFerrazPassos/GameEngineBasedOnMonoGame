using System;

namespace GameEngine.Utils;

internal static class SpriteUtils
{
    public static (int x, int y) ConvertNumberToXY(int number, int rows, int columns)
    {
        var max = rows * columns;
        if (number < 1 || number > max)
            throw new ArgumentOutOfRangeException(nameof(number), $"Number must be between 1 and {max}.");

        int x = (number - 1) % columns;
        int y = (number - 1) / columns;

        return (x, y);
    }
}