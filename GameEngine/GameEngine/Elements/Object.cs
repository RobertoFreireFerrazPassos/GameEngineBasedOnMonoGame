using GameEngine.Elements.Sprites;

namespace GameEngine.Elements;

internal abstract class Object
{
    public AnimatedSprite AnimatedSprite { get; set; }

    public abstract void Update();
}
