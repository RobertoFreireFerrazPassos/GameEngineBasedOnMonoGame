using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements;

internal abstract class Object
{
    public int X;

    public int Y;

    public int Speed;

    public AnimatedSprite AnimatedSprite;

    public Object(int x, int y, int speed, AnimatedSprite sprite)
    {
        X = x;
        Y = y;
        Speed = speed;
        AnimatedSprite = sprite;
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch batch, GameTime gameTime);
}
