using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

internal class MenuManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private SceneManager _sceneManager;

    public MenuManager(SpriteManager spriteManager, SceneManager sceneManager)
    {
        _spriteManager = spriteManager;
        _sceneManager = sceneManager;
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        if (InputUtils.IsKeyEnter())
        {
            _sceneManager.Scene = SceneEnum.GAME;
        }            
    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);
        batch.DrawString(_spriteManager.Font, "MENU", new Vector2(300, 200), Color.Yellow);
        batch.DrawString(_spriteManager.Font, "PRESS ENTER TO START GAME", new Vector2(300, 250), Color.Yellow);
        batch.End();
    }
}
