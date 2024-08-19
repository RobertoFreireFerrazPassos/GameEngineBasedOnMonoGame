using GameEngine.Elements.Managers;
using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements;

public abstract class SpriteObject
{
    public Vector2 Position;

    public int Speed;

    public AnimatedSprite AnimatedSprite;

    public CollisionBox CollisionBox;

    public SpriteObject(int x, int y)
    {
        Position = new Vector2(x, y);
    }

    public virtual void Draw(SpriteBatch batch, GameTime gameTime)
    {
        if (!AnimatedSprite.Visibility.Visible)
        {
            return;
        }

        var offset = Camera.Position;        
        AnimatedSprite.Update(gameTime);
        var position = new Vector2(
            Position.X + (int)offset.X,
            Position.Y + (int)offset.Y
        );

        batch.Draw(
            TextureManager.Texture2D[AnimatedSprite.Texture.TextureKey],
            position,
            AnimatedSprite.GetSourceRectangle(),
            AnimatedSprite.Color,
            0,
            new Vector2(1, 1),
            new Vector2(1, 1),
            AnimatedSprite.FlipHorizontally,
            0f
        );
    }

    public virtual Rectangle GetBox()
    {
        return new Rectangle((int)Position.X + CollisionBox.X, (int)Position.Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
    }
}
