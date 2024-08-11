using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Managers;

internal class SpriteManager
{
    private GraphicsDeviceManager _graphics;

    public SpriteFont Font;

    public SpriteBatch SpriteBatch;

    public SpriteManager(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
    }

    public void LoadSpriteBatch(GraphicsDevice graphicsDevice, SpriteFont font)
    {
        Font = font;
        SpriteBatch = new SpriteBatch(graphicsDevice);
    }

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