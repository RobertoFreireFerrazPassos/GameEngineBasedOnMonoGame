using GameEngine.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class GameManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private SceneManager _sceneManager;
    private List<Player> _players = new List<Player>();
    private List<Enemy> _enemies = new List<Enemy>();
    private TileMap _tileMap = new TileMap();

    public GameManager(SpriteManager spriteManager, TextureManager textureManager, SceneManager sceneManager)
    {
        _spriteManager = spriteManager;
        _textureManager = textureManager;
        _sceneManager = sceneManager;
    }

    public void LoadContent()
    {
        _textureManager.AddTexture(GameConstants.Constants.Sprite.Sprites, 3, 13);
        LoadGame();
    }

    private void LoadGame()
    {
        var spriteTexture = _textureManager.Textures.GetValueOrDefault(GameConstants.Constants.Sprite.Sprites);
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

            if (!ply.Alive)
            {
                batch.DrawString(_spriteManager.Font, "You Died", new Vector2(100, 200), Color.Red);
            }
        }

        foreach (var eny in _enemies)
        {
            eny.Draw(batch, gameTime);
        }
        
        batch.End();
    }
}
