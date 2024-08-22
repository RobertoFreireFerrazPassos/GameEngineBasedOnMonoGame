using GameEngine.Elements;
using GameEngine.Elements.Managers;
using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.GameObjects.Elements;

public class Player : SpriteObject
{
    private bool _isTakingDamage;
    private float _damageTime;

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
                        FrameDuration = 0.3f,
                        Loop = false
                    }
                },
                {
                    AnimationEnum.UP,
                    new Animation()
                    {
                        Frames = new int[] { 4, 5 },
                        FrameDuration = 0.3f,
                        Loop = true
                    }
                },
                {
                    AnimationEnum.MOVING,
                    new Animation()
                    {
                        Frames = new int[] { 2, 3 },
                        FrameDuration = 0.3f,
                        Loop = true
                    }
                }
            };

        AnimatedSprite = new AnimatedSprite(
                    Color.White
                    , animations
                    , AnimationEnum.IDLE
                    , new GameEngine.Elements.Texture("world", 40, 26, 13, 40, 40)
                );
        AnimatedSprite.Ordering.Z = 1;
        CollisionBox = new CollisionBox(2, 2, 38, 36);
    }

    public void Update(float deltaTime, List<Enemy> enemies)
    {
        deltaTime = deltaTime / (float) Config.SixtyFramesASecond;

        var direction = Vector2.Zero;

        if (_damageTime <= 0f)
        {
            _damageTime = 0f;
            _isTakingDamage = false;
        }
        else
        {
            _damageTime-= deltaTime;
        }

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

            Position.X += direction.X * Speed * deltaTime;

            if (TileMapManager.IsCollidingWith(GetBox()))
            {
                Position.X = tempX;
            }

            Position.Y += direction.Y * Speed * deltaTime;

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

    public void ReceivesDamage()
    {
        _isTakingDamage = true;
        _damageTime = 1f;
    }

    public override void Draw(SpriteBatch batch, float deltaTime, Color? color = null)
    {
        if (_isTakingDamage)
        {
            color = new Color(220, 20, 60);
        }

        base.Draw(batch, deltaTime, color);
    }
}
