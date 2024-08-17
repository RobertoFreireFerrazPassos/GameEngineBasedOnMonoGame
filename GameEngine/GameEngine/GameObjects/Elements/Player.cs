using GameEngine.Elements;
using GameEngine.Elements.Managers;
using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.GameObjects.Elements;

public class Player : SpriteObject
{
    public Player(int x, int y) : base(x, y)
    {
        Speed = 2;
        var animations = new Dictionary<AnimationEnum, Animation>
            {
                {
                    AnimationEnum.IDLE,
                    new Animation()
                    {
                        Frames = new int[] { 2 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = false
                    }
                },
                {
                    AnimationEnum.UP,
                    new Animation()
                    {
                        Frames = new int[] { 4, 5 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = true
                    }
                },
                {
                    AnimationEnum.MOVING,
                    new Animation()
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
                    , 40
                );
        AnimatedSprite.Ordering.Z = 1;
        CollisionBox = new CollisionBox(2, 2, 38, 36);
    }

    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        var direction = Vector2.Zero;
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / Config.SixtyFramesASecond);

        SetDirection();
        UpdateAnimation();

        Speed = 2;
        foreach (var enemy in enemies)
        {
            if (GetBox().Intersects(enemy.GetBox()))
            {
                Speed = 1;
            }
        }

        void SetDirection()
        {
            if (InputUtils.IsKeyDown(InputEnum.UP))
            {
                direction.Y -= 1;
            }

            if (InputUtils.IsKeyDown(InputEnum.DOWN))
            {
                direction.Y += 1;
            }

            if (InputUtils.IsKeyDown(InputEnum.RIGHT))
            {
                direction.X += 1;
            }

            if (InputUtils.IsKeyDown(InputEnum.LEFT))
            {
                direction.X -= 1;
            }

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            var tempX = Position.X;
            var tempY = Position.Y;

            Position.X += direction.X * Speed * elapsedTime;

            if (TileMapManager.IsCollidingWith(GetBox()))
            {
                Position.X = tempX;
            }

            Position.Y += direction.Y * Speed * elapsedTime;

            if (TileMapManager.IsCollidingWith(GetBox()))
            {
                Position.Y = tempY;
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
