using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements;

internal class Player : Object
{
    public bool Alive = true;

    public Player(int x, int y, Texture texture) : base(x, y)
    {
        Speed = 2;

        var animations = new Dictionary<string, Sprites.Animation>
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
                    , animations
                    , Constants.Animation.Idle
                );
        CollisionBox = new CollisionBox(2, 2, 36, 36);        
    }

    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        var direction = Vector2.Zero;
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / Constants.Config.SixtyFramesASecond);

        SetDirection();
        UpdateAnimation();

        foreach (var enemy in enemies)
        {
            if (GetBox().Intersects(enemy.GetBox()))
            {
                Alive = false;
            }
        }

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

    public override void Draw(SpriteBatch batch, GameTime gameTime, SpriteFont font)
    {
        if (!Alive)
        {
            batch.DrawString(font, "You Died", new Vector2(100, 200), Color.Red);
        }
        
        base.Draw(batch, gameTime, font);
    }
}
