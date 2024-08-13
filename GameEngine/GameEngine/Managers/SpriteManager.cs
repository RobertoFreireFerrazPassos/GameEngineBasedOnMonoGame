using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Managers;

public static class SpriteManager
{
    private static GraphicsDeviceManager _graphics;

    public static SpriteFont Font;

    public static SpriteBatch SpriteBatch;

    public static void LoadSpriteManager(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
    }

    public static void LoadSpriteBatch(GraphicsDevice graphicsDevice, SpriteFont font)
    {
        Font = font;
        SpriteBatch = new SpriteBatch(graphicsDevice);
    }

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