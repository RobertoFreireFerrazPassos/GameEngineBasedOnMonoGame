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
        
        _player = new Player(0, 0);
        FollowCamera.LoadFollowCamera(Vector2.Zero);
        _enemies.Add(new Enemy(50, 50));
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

        FollowCamera.Follow(_player.GetBox(), new Vector2(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight));

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
