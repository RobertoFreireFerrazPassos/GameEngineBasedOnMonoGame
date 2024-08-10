using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static GameEngine.Enums.Constants;

namespace GameEngine.Elements;

internal class Player : Object
{
    public Player(int x, int y, Texture texture) : base(x, y)
    {
        Speed = 2;

        var playerAnimations = new Dictionary<string, Sprites.Animation>
            {
                {
                    Constants.Animation.Idle,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 2 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = false
                    }
                },
                {
                    Constants.Animation.Up,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 4, 5 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = true
                    }
                },
                {
                    Constants.Animation.Moving,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 2, 3 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = true
                    }
                }
            };

        AnimatedSprite = new AnimatedSprite(
                    texture
                    , Color.White
                    , playerAnimations
                    , Constants.Animation.Idle
                );
        CollisionBox = new CollisionBox(2, 2, 36, 36);        
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
