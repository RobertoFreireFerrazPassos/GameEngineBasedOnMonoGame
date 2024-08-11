using GameEngine.Elements.Sprites;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GameEngine.Enums.Constants;

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

    public virtual void Draw(SpriteBatch batch, GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteUtils.ConvertNumberToXY(AnimatedSprite);
        batch.Draw(AnimatedSprite.Texture.Texture2D, new Vector2(X, Y), new Rectangle(x * Sprite.Pixels, y * Sprite.Pixels, Sprite.Pixels, Sprite.Pixels), AnimatedSprite.Color, 0, new Vector2(1, 1), new Vector2(1, 1), AnimatedSprite.FlipHorizontally, 0f);
    }

    public Rectangle GetBox()
    {
        return new Rectangle((int)X + CollisionBox.X, (int)Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
    }
}
