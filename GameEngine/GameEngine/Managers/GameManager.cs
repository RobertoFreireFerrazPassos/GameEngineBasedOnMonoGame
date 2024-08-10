using GameEngine.Enums;
using GameEngine.Elements;
using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class GameManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private List<Player> _players = new List<Player>();
    private List<Object> _objects = new List<Object>();
    private List<Enemy> _enemies = new List<Enemy>();
    private TileMap _tileMap = new TileMap();

    public GameManager(Game1 game, ContentManager content)
    {
        _spriteManager = new SpriteManager(game);
        _textureManager = new TextureManager(content);
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _spriteManager.LoadSpriteBatch(graphicsDevice);
        _textureManager.AddTexture(Constants.Sprite.Coin);
        LoadGame();
    }

    private void LoadGame()
    {
        _objects.Add(new Object()
        {
            AnimatedSprite = new AnimatedSprite(
                _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Coin),
                new Vector2(100, 100),
                Color.White)
        });
        _objects.Add(new Object()
        {
            AnimatedSprite = new AnimatedSprite(
                _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Coin),
                new Rectangle(100, 164, 256, 64),
                Color.White)
        });
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;

        batch.Begin();

        foreach (var ply in _players)
        {
            ply.AnimatedSprite?.Draw(batch);
        }

        foreach (var obj in _objects)
        {
            obj.AnimatedSprite?.Draw(batch);
        }

        foreach (var eny in _enemies)
        {
            eny.AnimatedSprite?.Draw(batch);
        }

        batch.End();
    }
}
