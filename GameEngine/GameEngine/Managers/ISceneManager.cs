using Microsoft.Xna.Framework;

namespace GameEngine.Managers;

public interface ISceneManager
{
    public void LoadContent();

    public void Update(GameTime gameTime);

    public void Draw(GameTime gameTime);
}
