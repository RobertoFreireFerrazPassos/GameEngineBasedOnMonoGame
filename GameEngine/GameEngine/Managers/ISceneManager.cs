using Microsoft.Xna.Framework;

namespace GameEngine.Managers;

internal interface ISceneManager
{
    public void LoadContent();

    public void Update(GameTime gameTime);

    public void Draw(GameTime gameTime);
}
