using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Elements;

public class Texture
{
    public Texture2D Texture2D;

    public int Pixels;

    public int Rows;

    public int Columns;

    public (int x, int y) ConvertNumberToXY(int number)
    {
        var max = Rows * Columns;
        if (number < 1 || number > max)
            throw new ArgumentOutOfRangeException(nameof(number), $"Number must be between 1 and {max}.");

        int x = (number - 1) % Columns;
        int y = (number - 1) / Columns;

        return (x, y);
    }
}