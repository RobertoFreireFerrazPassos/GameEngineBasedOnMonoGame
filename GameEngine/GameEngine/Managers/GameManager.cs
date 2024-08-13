using GameEngine.Elements;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.Managers;

public class GameManager : ISceneManager
{
    private Player _player;
    private List<Enemy> _enemies = new List<Enemy>();
    private GraphicsDeviceManager _graphicsDeviceManager;

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
        _player = new Player(8 * pixels, 3 * pixels);
        _enemies.Add(new Enemy(8 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(9 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(3 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(4 * pixels, 8 * pixels));
    }

    public void Update(GameTime gameTime)
    {
        if (InputUtils.IsKeyDown(InputEnum.ENTER))
        {
            SceneManager.Scene = SceneEnum.MENU;
        }

        _player.Update(gameTime, _enemies); 
        foreach (var eny in _enemies)
        {
            eny.Update(gameTime, _player, _enemies);
        }
    }

    public void Draw(GameTime gameTime)
    {
        var batch = SpriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);

        TileMapManager.Draw(batch, gameTime);

        _player.Draw(batch, gameTime);

        foreach (var eny in _enemies)
        {
            eny.Draw(batch, gameTime);
        }
        
        batch.End();
    }
}
