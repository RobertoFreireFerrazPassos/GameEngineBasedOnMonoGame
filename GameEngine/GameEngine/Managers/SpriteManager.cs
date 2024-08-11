using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
}