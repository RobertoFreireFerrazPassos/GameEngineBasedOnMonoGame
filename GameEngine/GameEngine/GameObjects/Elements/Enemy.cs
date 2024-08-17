using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using GameEngine.Enums;
using static GameEngine.GameConstants.Constants;
using GameEngine.Elements.Managers;
using GameEngine.Elements;

namespace GameEngine.GameObjects.Elements;

public class Enemy : SpriteObject
{
    private int _state; // 0- far 1- wall  2-find
    private Vector2 _playerPosition;
    private Vector2 _direction = Vector2.Zero;
    private const float MovingTime = 0.5f;
    private float _movingTime = MovingTime;
    private float minDist = 1f;
    private float maxDist = 6f;

    public Enemy(int x, int y) : base(x, y)
    {
        Speed = 1;

        var animations = new Dictionary<AnimationEnum, Animation>
            {
                {
                    AnimationEnum.IDLE,
                    new Animation()
                    {
                        Frames = new int[] { 6 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = false
                    }
                }
            };

        AnimatedSprite = new AnimatedSprite(
                    Color.White
                    , animations
                    , AnimationEnum.IDLE
                    , 40
                );
        AnimatedSprite.Ordering.Z = 2;
        CollisionBox = new CollisionBox(6, 17, 28, 18);
    }

    public void Update(GameTime gameTime, Player player, List<Enemy> enemies)
    {
        if (_state == 0 || _state == 1)
        {
            FindPlayer(player);
        }
        else if (_state == 2)
        {
            Move(gameTime, enemies);
        }
    }

    private void FindPlayer(Player player)
    {
        var playerCenter = player.GetBox().Center.ToVector2();
        var enemyCenter = GetBox().Center.ToVector2();
        var distance = Vector2.Distance(playerCenter, enemyCenter);

        if (distance < minDist * AnimatedSprite.Pixels)
        {
            return;
        }

        if (distance > maxDist * AnimatedSprite.Pixels)
        {
            _state = 0;
            return;
        }

        if (IsObstacleBetween(enemyCenter, playerCenter, 4, CheckForObstacle))
        {
            _state = 1;
            return;
        }

        _playerPosition = playerCenter;
        _state = 2;
    }

    private bool CheckForObstacle(Vector2 position)
    {
        return TileMapManager.IsCollidingWith(position);
    }

    private bool IsObstacleBetween(Vector2 start, Vector2 end, int stepSize, Func<Vector2, bool> obstacleCheck)
    {
        // Calculate the direction and length of the line
        var direction = end - start;
        float length = direction.Length();
        direction.Normalize(); // Normalize to get a direction vector

        // Check points along the line every 'stepSize' pixels
        for (float distance = 0; distance <= length; distance += stepSize)
        {
            // Calculate the current point on the line
            Vector2 currentPoint = start + direction * distance;

            // Check if there is an obstacle at the current point
            if (obstacleCheck(currentPoint))
            {
                return true; // An obstacle was found
            }
        }

        return false; // No obstacle was found along the line
    }

    private void Move(GameTime gameTime, List<Enemy> enemies)
    {
        var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _movingTime -= seconds;

        if (_movingTime <= 0)
        {
            _movingTime = MovingTime;
            _direction = Vector2.Zero;
            _state = 0;
        }

        if (_direction == Vector2.Zero)
        {
            CreateNewDirection();
        }

        UpdatePosition(gameTime, enemies);
    }

    private void CreateNewDirection()
    {
        var diffX = _playerPosition.X - Position.X;
        var diffY = _playerPosition.Y - Position.Y;

        if (Math.Abs(diffX) > Math.Abs(diffY))
        {
            _direction = new Vector2(Math.Sign(diffX), 0);
        }
        else
        {
            _direction = new Vector2(0, Math.Sign(diffY));
        }
    }

    private void UpdatePosition(GameTime gameTime, List<Enemy> enemies)
    {
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / Config.SixtyFramesASecond);

        var tempX = Position.X;
        var tempY = Position.Y;

        Position.X += _direction.X * Speed * elapsedTime;

        if (TileMapManager.IsCollidingWith(GetBox()) || DetectCollisionWithEnemies(enemies))
        {
            Position.X = tempX;
        }

        Position.Y += _direction.Y * Speed * elapsedTime;

        if (TileMapManager.IsCollidingWith(GetBox()) || DetectCollisionWithEnemies(enemies))
        {
            Position.Y = tempY;
        }
    }

    private bool DetectCollisionWithEnemies(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != this && GetBox().Intersects(enemy.GetBox()))
            {
                return true;
            }
        }

        return false;
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
        if (_state == 2)
        {
            DrawLineToPlayer();
        }
        base.Draw(batch, gameTime);
    }

    private void DrawLineToPlayer()
    {
        var offset = Camera.Position;
        var enemyCenter = GetBox().Center.ToVector2();
        SpriteManager.DrawLine(
            new Vector2(
                enemyCenter.X + (int)offset.X,
                enemyCenter.Y + (int)offset.Y
            ),
            new Vector2(
                _playerPosition.X + (int)offset.X,
                _playerPosition.Y + (int)offset.Y
            ),
            Color.Red,
            2);
    }
}
