using GameEngine.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.Managers;

internal class GameManager : ISceneManager
{
    private Player _player;
    private List<Enemy> _enemies = new List<Enemy>();
    private GraphicsDeviceManager _graphicsDeviceManager;
    private bool _firstTime = true;

    public GameManager(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
    }

    public void LoadContent()
    {
        TextureManager.AddTexture(Sprite.Sprites, 26, 13);
        TileMapManager.LoadTileMap("../../../Data/tilemap.csv");
        var pixels = Sprite.Pixels;
        TileMapManager.TextureStore = new()
        {
            new Rectangle(0 * pixels, 1 * pixels, pixels, pixels)
        };
        Camera.LoadCamera(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
        _player = new Player(pixels, pixels);
        _enemies.Add(new Enemy(pixels, 3 * pixels));
        _enemies.Add(new Enemy(2 *pixels, 3 * pixels));
    }

    public void Update(GameTime gameTime)
    {
        _player.Update(gameTime, _enemies); 
        foreach (var eny in _enemies)
        {
            eny.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime)
    {
        var batch = SpriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);

        TileMapManager.Draw(batch, gameTime);

        _player.Draw(batch, gameTime);

        if (!_player.Alive)
        {
            batch.DrawString(SpriteManager.Font, "You Died", new Vector2(350, 200), Color.Red);
        }

        foreach (var eny in _enemies)
        {
            eny.Draw(batch, gameTime);
        }
        
        batch.End();
    }
}
