using GameEngine.Elements.Sprites;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
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

    public virtual void Draw(SpriteBatch batch, GameTime gameTime)
    {
        var offset = FollowCamera.Position;
        var spriteTexture = TextureManager.Texture;
        var pixels = Sprite.Pixels;
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteManager.ConvertNumberToXY(
            AnimatedSprite.Sprite,
            spriteTexture.Rows,
            spriteTexture.Columns);
        var box = GetBox();
        var position = new Vector2(
            X + (int)offset.X,
            Y + (int)offset.Y
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

    public Rectangle GetBox()
    {
        return new Rectangle((int)X + CollisionBox.X, (int)Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
    }
}
