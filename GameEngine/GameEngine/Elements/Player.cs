using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static GameEngine.Enums.Constants;

namespace GameEngine.Elements;

internal class Player : Object
{
    public Player(int x, int y, int speed, AnimatedSprite sprite) : base(x, y, speed, sprite)
    {
    }

    public override void Update(GameTime gameTime)
    {
        AnimatedSprite.FlipHorizontally = SpriteEffects.None;
        var state = Keyboard.GetState();
        var direction = Vector2.Zero;

        if (state.IsKeyDown(Keys.Down))
        {
            direction.Y += 1;
            AnimatedSprite.SetState(Constants.Animation.Moving);
        }

        if (state.IsKeyDown(Keys.Right))
        {
            direction.X += 1;
            AnimatedSprite.SetState(Constants.Animation.Moving);
        }

        if (state.IsKeyDown(Keys.Left))
        {
            direction.X -= 1;
            AnimatedSprite.SetState(Constants.Animation.Moving);
            AnimatedSprite.FlipHorizontally = SpriteEffects.FlipHorizontally;
        }

        if (state.IsKeyDown(Keys.Up))
        {
            direction.Y -= 1;
            AnimatedSprite.SetState(Constants.Animation.Up);
        }

        if (!state.IsKeyDown(Keys.Down) && !state.IsKeyDown(Keys.Up)
            && !state.IsKeyDown(Keys.Left) && !state.IsKeyDown(Keys.Right))
        {
            AnimatedSprite.SetState(Constants.Animation.Idle);
        }

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
        }

        X += direction.X * Speed;
        Y += direction.Y * Speed;
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteUtils.ConvertNumberToXY(AnimatedSprite);
        batch.Draw(AnimatedSprite.Texture.Texture2D, new Vector2(X, Y), new Rectangle(x * Sprite.Pixels, y * Sprite.Pixels, Sprite.Pixels, Sprite.Pixels), AnimatedSprite.Color, 0, new Vector2(1, 1), new Vector2(1, 1), AnimatedSprite.FlipHorizontally, 0f);
    }
}
