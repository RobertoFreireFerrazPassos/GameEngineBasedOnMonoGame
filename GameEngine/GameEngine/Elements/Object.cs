using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements;

internal abstract class Object
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Speed { get; set; }

    public AnimatedSprite AnimatedSprite { get; set; }

    public Object(int x, int y, int speed, AnimatedSprite sprite)
    {
        X = x;
        Y = y;
        Speed = speed;
        AnimatedSprite = sprite;
    }

    public abstract void Update();

    public abstract void Draw(SpriteBatch batch);
}
