using GameEngine.Enums;
using GameEngine.Elements;
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
        _textureManager.AddTexture(Constants.Sprite.Sprites, 3, 13);
        LoadGame();
    }

    private void LoadGame()
    {
        var spriteTexture = _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Sprites);
        _players.Add(new Player(0, 0, spriteTexture));
        _enemies.Add(new Enemy(50, 50, spriteTexture));
    }

    public void Update(GameTime gameTime)
    {
        foreach (var ply in _players)
        {
            ply.Update(gameTime, _enemies);
        }

        foreach (var eny in _enemies)
        {
            eny.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;

        batch.Begin(samplerState: SamplerState.PointClamp);

        foreach (var ply in _players)
        {
            ply.Draw(batch, gameTime);
        }

        foreach (var eny in _enemies)
        {
            eny.Draw(batch, gameTime);
        }

        batch.End();
    }
}
