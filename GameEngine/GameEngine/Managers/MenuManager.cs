using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

internal class MenuManager : ISceneManager
{
    public MenuManager()
    {
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        if (InputUtils.IsKeyEnter())
        {
            SceneManager.Scene = SceneEnum.GAME;
        }            
    }

    public void Draw(GameTime gameTime)
    {
        var batch = SpriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);
        batch.DrawString(SpriteManager.Font, "MENU", new Vector2(300, 200), Color.Yellow);
        batch.DrawString(SpriteManager.Font, "PRESS ENTER TO START GAME", new Vector2(300, 250), Color.Yellow);
        batch.End();
    }
}
