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

    public void LoadContent()
    {
        uint pixelsInUInt = 40;
        int pixels = (int)pixelsInUInt;
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
            "world",
            MapLayerEnum.Foreground,
            0 * pixelsInUInt,
            0 * pixelsInUInt
        );
        TileMapManager.AddTileMap(
            "hiddenplace1",
            "../../../Tilemaps/HiddenPlace1.csv",
            "world",
            MapLayerEnum.Parallax,
            10 * pixelsInUInt,
            3 * pixelsInUInt
        );
        Camera.LoadCamera();
        _player = new Player(22 * pixels, 20 * pixels);

        for (int i = 27; i <= 27; i++)
        {
            for (int j = 16; j <= 16; j++)
            {
                _enemies.Add(new Enemy(i * pixels, j * pixels));
            }
        }
        _objects.Add(new StaticObject(22 * pixels, 18 * pixels));

        var melody = "B15G30G5F5E5D10C20";
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
        Camera.UpdateForFollowPosition(_player.GetBox().Center.ToVector2(), 0.05f);
    }

    public void Draw(GameTime gameTime)
    {

        var normalMapEffect = TextureManager.Effects["normalMap"];
        var spriteTexture = TextureManager.Texture2D["world"];
        var normalMapTexture = TextureManager.Texture2D["normalMap"];

        normalMapEffect.Parameters["TextureSampler"].SetValue(spriteTexture);
        normalMapEffect.Parameters["NormalMapSampler"].SetValue(normalMapTexture);
        normalMapEffect.Parameters["LightDirection"].SetValue(new Vector3(1.0f, 1.0f, 1.0f));
        normalMapEffect.Parameters["LightColor"].SetValue(new Vector3(1.0f, 1.0f, 1.0f));

        var batch = SpriteManager.SpriteBatch;
        batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, normalMapEffect);

        TileMapManager.Draw(MapLayerEnum.Background, batch, gameTime);
        TileMapManager.Draw(MapLayerEnum.Foreground, batch, gameTime);

        var objSrites = new List<SpriteObject>();
        objSrites.Add(_player);
        objSrites.AddRange(_enemies);
        objSrites.AddRange(_objects);
        objSrites = objSrites
            .OrderBy(obj => obj.AnimatedSprite.Ordering.IsSortable ? obj.Position.Y : obj.AnimatedSprite.Ordering.Z)
            .ToList();

        foreach (var obj in objSrites)
        {
            obj.Draw(batch, gameTime);
        }

        TileMapManager.Draw(MapLayerEnum.Parallax, batch, gameTime);

        batch.End();
    }
}
