using GameEngine.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class GameManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private SceneManager _sceneManager;
    private Player _player;
    private List<Enemy> _enemies = new List<Enemy>();
    private TileMap _tileMap = new TileMap();
    private FollowCamera _followCamera;
    private GraphicsDeviceManager _graphicsDeviceManager;
    private bool _firstTime = true;

    public GameManager(GraphicsDeviceManager graphicsDeviceManager, SpriteManager spriteManager, SceneManager sceneManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        _spriteManager = spriteManager;
        _sceneManager = sceneManager;
    }

    public void LoadContent()
    {
        TextureManager.AddTexture(GameConstants.Constants.Sprite.Sprites, 26, 13);
        LoadGame();
    }

    private void LoadGame()
    {
        var spriteTexture = TextureManager.Textures.GetValueOrDefault(GameConstants.Constants.Sprite.Sprites);
        _player = new Player(0, 0, spriteTexture);
        _followCamera = new FollowCamera(Vector2.Zero);
        _enemies.Add(new Enemy(50, 50, spriteTexture));
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
        var batch = _spriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);

        _followCamera.Follow(_player.GetBox(), new Vector2(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight));
        _player.Draw(batch, gameTime, _followCamera.Position);

        if (!_player.Alive)
        {
            batch.DrawString(_spriteManager.Font, "You Died", new Vector2(350, 200), Color.Red);
        }

        foreach (var eny in _enemies)
        {
            eny.Draw(batch, gameTime, _followCamera.Position);
        }
        
        batch.End();
    }
}
