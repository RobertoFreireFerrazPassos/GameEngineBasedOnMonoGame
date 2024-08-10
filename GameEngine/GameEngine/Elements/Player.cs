using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GameEngine.Enums.Constants;

namespace GameEngine.Elements;

internal class Player : Object
{
    public Player(int x, int y, int speed, AnimatedSprite sprite) : base(x, y, speed, sprite)
    {
    }

    public override void Update(GameTime gameTime)
    {
        var direction = Vector2.Zero;
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / Constants.Config.SixtyFramesASecond);

        SetDirection();
        UpdateAnimation();

        void SetDirection()
        {
            var input = new InputUtils();

            if (input.IsKeyUp())
            {
                direction.Y -= 1;
            }

            if (input.IsKeyDown())
            {
                direction.Y += 1;
            }

            if (input.IsKeyRight())
            {
                direction.X += 1;
            }

            if (input.IsKeyLeft())
            {
                direction.X -= 1;
            }
            

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            
            X += direction.X * Speed * elapsedTime;
            Y += direction.Y * Speed * elapsedTime;
        }

        void UpdateAnimation()
        {
            if (direction.Y == 0 && direction.X == 0)
            {
                AnimatedSprite.SetState(Constants.Animation.Idle);
            }
            else
            {
                AnimatedSprite.FlipHorizontally = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                if (direction.Y < 0)
                {
                    AnimatedSprite.SetState(Constants.Animation.Up);
                }
                else
                {
                    AnimatedSprite.SetState(Constants.Animation.Moving);
                }
            }
        }
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteUtils.ConvertNumberToXY(AnimatedSprite);
        batch.Draw(AnimatedSprite.Texture.Texture2D, new Vector2(X, Y), new Rectangle(x * Sprite.Pixels, y * Sprite.Pixels, Sprite.Pixels, Sprite.Pixels), AnimatedSprite.Color, 0, new Vector2(1, 1), new Vector2(1, 1), AnimatedSprite.FlipHorizontally, 0f);
    }
}
