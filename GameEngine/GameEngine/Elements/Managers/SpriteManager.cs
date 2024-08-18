using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Elements.Managers;

public static class SpriteManager
{
    private static GraphicsDeviceManager _graphics;

    public static SpriteFont Font;

    public static SpriteBatch SpriteBatch;

    private static Texture2D _pixelTexture;

    public static void LoadSpriteManager(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
    }

    public static void LoadSpriteBatch(GraphicsDevice graphicsDevice, SpriteFont font)
    {
        Font = font;
        SpriteBatch = new SpriteBatch(graphicsDevice);
        _pixelTexture = new Texture2D(graphicsDevice, 1, 1);
        _pixelTexture.SetData(new Color[] { Color.White });
    }

    public static void DrawLine(Vector2 start, Vector2 end, Color color, float thickness)
    {
        // Calculate the distance and angle
        float distance = Vector2.Distance(start, end);
        float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

        // Draw the line as a rectangle
        SpriteBatch.Draw(_pixelTexture, start, null, color, angle, Vector2.Zero, new Vector2(distance, thickness), SpriteEffects.None, 0);
    }
}