using GameEngine.Elements.Sprites;
using GameEngine.GameConstants;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GameEngine.GameConstants.Constants;

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

    public virtual void Draw(SpriteBatch batch, GameTime gameTime, Vector2 offset)
    {
        var pixels = Constants.Sprite.Pixels;
        AnimatedSprite.Update(gameTime);
        int number = AnimatedSprite.Sprite;
        int rows = AnimatedSprite.Texture.Rows;
        int columns = AnimatedSprite.Texture.Columns;
        (int x, int y) = SpriteManager.ConvertNumberToXY(number, rows, columns);

        var box = GetBox();
        var position = new Vector2(
            X + (int)offset.X,
            Y + (int)offset.Y
        );

        batch.Draw(
            AnimatedSprite.Texture.Texture2D,
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

    public Rectangle GetBox()
    {
        return new Rectangle((int)X + CollisionBox.X, (int)Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
    }
}
