using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

internal class SpriteManager
{
    private GraphicsDeviceManager _graphics;

    public SpriteBatch SpriteBatch;

    public SpriteManager(Game game)
    {
        _graphics = new GraphicsDeviceManager(game);
    }

    public void LoadSpriteBatch(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new SpriteBatch(graphicsDevice);
    }
}