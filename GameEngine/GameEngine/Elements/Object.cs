using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements;

internal abstract class Object
{
    public float X;

    public float Y;

    public int Speed;

    public AnimatedSprite AnimatedSprite;

    public CollisionBox CollisionBox;

    public Object(int x, int y)
    {
        X = x;
        Y = y;
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch batch, GameTime gameTime);
}
