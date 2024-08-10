using GameEngine.Enums;
using GameEngine.Elements;
using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

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
        var playerAnimation = new Dictionary<string, Animation>();
        playerAnimation.Add(Constants.Animation.Idle, new Animation()
        {
            Frames = new int[] { 2 },
            FrameDuration = TimeSpan.FromMilliseconds(300),
            Loop = false
        });
        playerAnimation.Add(Constants.Animation.Up, new Animation()
        {
            Frames = new int[] { 4, 5 },
            FrameDuration = TimeSpan.FromMilliseconds(300),
            Loop = true
        });
        playerAnimation.Add(Constants.Animation.Moving, new Animation()
        {
            Frames = new int[] { 2, 3 },
            FrameDuration = TimeSpan.FromMilliseconds(300),
            Loop = true
        });

        _players.Add(new Player(0, 0, 2, 
            new AnimatedSprite(
                _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Sprites)
                , Color.White
                , playerAnimation
                , Constants.Animation.Idle))
            );
    }

    public void Update(GameTime gameTime)
    {
        foreach (var ply in _players)
        {
            ply.Update(gameTime);
        }

        foreach (var eny in _enemies)
        {
            eny.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;

        batch.Begin();

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
