using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

internal class StartManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private SceneManager _sceneManager; 
    private float timer;

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
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer >= 3f)
        {
            _sceneManager.Scene = SceneEnum.MENU;
            timer = 0f;
        }
    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);
        batch.DrawString(_spriteManager.Font, "PRIEST VS DEMONS", new Vector2(300, 200), Color.Red);
        batch.DrawString(_spriteManager.Font, "BY ROBERTO FREIRE", new Vector2(350, 250), Color.Red);
        batch.End();
    }
}
