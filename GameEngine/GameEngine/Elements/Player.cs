using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Managers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements;

internal class Player : Object
{
    public bool Alive = true;

    public Player(int x, int y) : base(x, y)
    {
        Speed = 2;

        var animations = new Dictionary<AnimationEnum, Animation>
            {
                {
                    AnimationEnum.IDLE,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 2 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = false
                    }
                },
                {
                    AnimationEnum.UP,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 4, 5 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = true
                    }
                },
                {
                    AnimationEnum.MOVING,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 2, 3 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = true
                    }
                }
            };

        AnimatedSprite = new AnimatedSprite(
                    Color.White
                    , animations
                    , AnimationEnum.IDLE
                );
        CollisionBox = new CollisionBox(2, 2, 38, 36);        
    }

    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        var direction = Vector2.Zero;
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / GameConstants.Constants.Config.SixtyFramesASecond);

        SetDirection();
        UpdateAnimation();

        foreach (var enemy in enemies)
        {
            if (GetBox().Intersects(enemy.GetBox()))
            {
                Alive = false;
            }
        }

        Camera.Update(X, Y);

        void SetDirection()
        {
            if (InputUtils.IsKeyUp())
            {
                direction.Y -= 1;
            }

            if (InputUtils.IsKeyDown())
            {
                direction.Y += 1;
            }

            if (InputUtils.IsKeyRight())
            {
                direction.X += 1;
            }

            if (InputUtils.IsKeyLeft())
            {
                direction.X -= 1;
            }
            

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            var tempX = X;
            var tempY = Y;

            X += direction.X * Speed * elapsedTime;

            if (TileMapManager.IsCollidingWithTiles(GetBox()))
            {
                X = tempX;
            }

            Y += direction.Y * Speed * elapsedTime;

            if (TileMapManager.IsCollidingWithTiles(GetBox()))
            {
                Y = tempY;
            }
        }

        void UpdateAnimation()
        {
            if (direction.Y == 0 && direction.X == 0)
            {
                AnimatedSprite.SetState(AnimationEnum.IDLE);
            }
            else
            {
                AnimatedSprite.FlipHorizontally = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                if (direction.Y < 0)
                {
                    AnimatedSprite.SetState(AnimationEnum.UP);
                }
                else
                {
                    AnimatedSprite.SetState(AnimationEnum.MOVING);
                }
            }
        }
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {        
        base.Draw(batch, gameTime);
    }
}
