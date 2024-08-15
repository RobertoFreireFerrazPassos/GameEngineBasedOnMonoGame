using GameEngine.Elements.Sprites;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.Elements;

public abstract class Object
{
    public Vector2 Position;

    public int Speed;

    public AnimatedSprite AnimatedSprite;

    public CollisionBox CollisionBox;

    public Object(int x, int y)
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
        var spriteTexture = TextureManager.Texture;
        var pixels = AnimatedSprite.Pixels;
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteManager.ConvertNumberToXY(
            AnimatedSprite.Sprite,
            spriteTexture.Rows,
            spriteTexture.Columns);
        var box = GetBox();
        var position = new Vector2(
            Position.X + (int)offset.X,
            Position.Y + (int)offset.Y
        );

        batch.Draw(
            spriteTexture.Texture2D,
            position, 
            new Rectangle(x * pixels, y * pixels, pixels, pixels), 
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
