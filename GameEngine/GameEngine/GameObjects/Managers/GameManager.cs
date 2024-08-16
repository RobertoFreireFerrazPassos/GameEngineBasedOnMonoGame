using GameEngine.Elements;
using GameEngine.Elements.Managers;
using GameEngine.Enums;
using GameEngine.GameObjects.Elements;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.GameObjects.Managers;

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
        var pixels = 40;
        TextureManager.AddTexture("Sprite-0001", 26, 13, pixels);
        TileMapManager.LoadTileMap(
            "../../../Tilemaps/tilemap.csv",
            new()
            {
                new Rectangle(0 * pixels, 1 * pixels, pixels, pixels)
            },
            pixels
        );
        Camera.LoadCamera(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
        _player = new Player(8 * pixels, 3 * pixels);
        _enemies.Add(new Enemy(8 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(9 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(3 * pixels, 8 * pixels));
        _enemies.Add(new Enemy(4 * pixels, 8 * pixels));
    }

    public void Update(GameTime gameTime)
    {
        if (InputUtils.IsKeyJustPressed(InputEnum.ENTER))
        {
            GlobalManager.Scene = SceneEnum.MENU;
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

        var objSrites = new List<SpriteObject>();
        objSrites.Add(_player);
        objSrites.AddRange(_enemies);
        objSrites = objSrites.OrderBy(obj => obj.AnimatedSprite.Ordering.Z).ToList();

        foreach (var obj in objSrites)
        {
            obj.Draw(batch, gameTime);
        }

        batch.End();
    }
}
