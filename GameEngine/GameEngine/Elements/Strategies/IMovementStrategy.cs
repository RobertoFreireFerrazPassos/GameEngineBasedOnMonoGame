using System.Collections.Generic;

namespace GameEngine.Elements.Strategies;

public interface IMovementStrategy
{
    public void Update(float elapsedTime, SpriteObject target, List<SpriteObject> allies);
}
