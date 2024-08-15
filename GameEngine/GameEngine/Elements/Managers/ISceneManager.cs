using Microsoft.Xna.Framework;

namespace GameEngine.Elements.Managers;

public interface ISceneManager
{
    public void LoadContent();

    public void Update(GameTime gameTime);

    public void Draw(GameTime gameTime);
}
