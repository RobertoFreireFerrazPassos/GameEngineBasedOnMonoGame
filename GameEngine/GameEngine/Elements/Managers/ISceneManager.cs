namespace GameEngine.Elements.Managers;

public interface ISceneManager
{
    public void LoadContent();

    public void Update(float elapsedTime);

    public void Draw(float elapsedTime);
}
