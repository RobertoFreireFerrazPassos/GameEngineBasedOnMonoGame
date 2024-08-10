using GameEngine.Elements.Sprites;
using System;

namespace GameEngine.Utils;

internal static class SpriteUtils
{
    public static (int x, int y) ConvertNumberToXY(AnimatedSprite sprite)
    {
        int number = sprite.Sprite;
        int rows = sprite.Texture.Rows;
        int columns = sprite.Texture.Columns;

        var max = rows * columns;
        if (number < 1 || number > max)
            throw new ArgumentOutOfRangeException(nameof(number), $"Number must be between 1 and {max}.");

        int x = (number - 1) % columns;
        int y = (number - 1) / columns;

        return (x, y);
    }
}