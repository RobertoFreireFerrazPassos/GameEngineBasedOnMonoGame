using GameEngine.Elements;
using GameEngine.Elements.Managers;
using GameEngine.Elements.Map;
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
    private List<StaticObject> _objects = new List<StaticObject>();
    private GraphicsDeviceManager _graphicsDeviceManager;

    public GameManager(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
    }

    public void LoadContent()
    {
        uint pixelsInUInt = 40;
        int pixels = (int)pixelsInUInt;

        TextureManager.AddTexture("Sprite-0001", 26, 13, pixels);
        TileMapManager.SetTileMapConfiguration(
            new()
            {
                new Tile()
                {
                    Texture = new Rectangle(0 * pixels, 1 * pixels, pixels, pixels)
                },
                new Tile()
                {
                    Texture = new Rectangle(0 * pixels, 1 * pixels, pixels, pixels),
                    Collidable = false
                }
            },
            pixels
        );
        TileMapManager.AddTileMap(
            "world",
            "../../../Tilemaps/Map.csv",
            MapLayerEnum.Foreground,
            0 * pixelsInUInt,
            0 * pixelsInUInt
        );
        TileMapManager.AddTileMap(
            "hiddenplace1",
            "../../../Tilemaps/HiddenPlace1.csv",
            MapLayerEnum.Parallax,
            10 * pixelsInUInt,
            3 * pixelsInUInt
        );
        Camera.LoadCamera(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
        _player = new Player(22 * pixels, 20 * pixels);
        _enemies.Add(new Enemy(30 * pixels, 14 * pixels));
        _objects.Add(new StaticObject(22 * pixels, 18 * pixels));

        var melody = new (Note, uint)[]
        {
            (Note.B, 5), (Note.G, 5),
            (Note.G, 5), (Note.F, 5),
            (Note.E, 5), (Note.D, 5),
            (Note.C, 5), 
        };

        MusicManager.AddMelody("Square", melody, Waveform.Square);
    }

    public void Update(GameTime gameTime)
    {
        if (InputUtils.IsKeyJustPressed(InputEnum.ENTER))
        {
            MusicManager.Play("Square");
            GlobalManager.Scene = SceneEnum.MENU;
        }

        _player.Update(gameTime, _enemies);
        foreach (var eny in _enemies)
        {
            eny.Update(gameTime, _player, _enemies);
        }
        Camera.Update(_player.Position);
    }

    public void Draw(GameTime gameTime)
    {
        var batch = SpriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);

        TileMapManager.Draw(MapLayerEnum.Background, batch, gameTime);
        TileMapManager.Draw(MapLayerEnum.Foreground, batch, gameTime);

        var objSrites = new List<SpriteObject>();
        objSrites.Add(_player);
        objSrites.AddRange(_enemies);
        objSrites.AddRange(_objects);
        objSrites = objSrites
            .OrderBy(obj => obj.AnimatedSprite.Ordering.Z)
            .OrderBy(obj => obj.AnimatedSprite.Ordering.IsSortable ? obj.Position.Y : float.MaxValue)
            .ToList();

        foreach (var obj in objSrites)
        {
            obj.Draw(batch, gameTime);
        }

        TileMapManager.Draw(MapLayerEnum.Parallax, batch, gameTime);

        batch.End();
    }
}
