using System.Collections.Generic;

namespace GameEngine.Elements.Strategies;

public interface IMovementStrategy
{
    public int PreviousState { get; set; }

    public int State { get; set; }

    public void Update(float elapsedTime, SpriteObject target, List<SpriteObject> allies);

    public void Draw();
}
