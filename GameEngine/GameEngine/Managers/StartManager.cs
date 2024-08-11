using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

internal class StartManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private SceneManager _sceneManager; 
    private float _timer;
    private float _opaque = 1f;
    private float _freezeTime = 3f;
    private float _fadeoutTime = 3f;

    public StartManager(SpriteManager spriteManager, TextureManager textureManager, SceneManager sceneManager)
    {
        _spriteManager = spriteManager;
        _textureManager = textureManager;
        _sceneManager = sceneManager;
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += seconds;

        if (_timer > _freezeTime)
        {
            ApplyFadeOutEffect(seconds);
        }

        if (_timer > _freezeTime + _fadeoutTime)
        {
            NextScreen();
        }
    }

    private void NextScreen()
    {
        _sceneManager.Scene = SceneEnum.MENU;
        _timer = 0f;
        _opaque = 1f;
    }

    private void ApplyFadeOutEffect(float seconds)
    {
        _opaque -= seconds / (_fadeoutTime);
    }

    public void Draw(GameTime gameTime)
    {
        var color = new Color(255, 0, 0, 255) * _opaque;
        var batch = _spriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);
        batch.DrawString(_spriteManager.Font, "PRIEST VS DEMONS", new Vector2(300, 200), color);
        batch.DrawString(_spriteManager.Font, "BY ROBERTO FREIRE", new Vector2(350, 250), color);
        batch.End();
    }
}
