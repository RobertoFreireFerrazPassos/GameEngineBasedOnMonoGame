using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using GameEngine.Enums;
using GameEngine.GameConstants;
using GameEngine.Managers;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.Elements;

public class Enemy : Object
{
    private int _state; // 0- far 1- wall  2-find
    private Vector2 _playerPosition;
    private Vector2 _direction = Vector2.Zero;
    private float _movingTime = 0.5f;

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
                    , Sprite.Pixels
                );
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
        var plyCenter = player.GetBox().Center.ToVector2();
        var enemyCenter = GetBox().Center.ToVector2();
        var distance = Vector2.Distance(plyCenter, enemyCenter);

        if (distance < 0.4f * Constants.Sprite.Pixels)
        {
            return;
        }

        if (distance > 4 * Constants.Sprite.Pixels)
        {
            _state = 0;
            return;
        }

        if (IsObstacleBetween(enemyCenter, plyCenter, 4, CheckForObstacle))
        {
            _state = 1;
            return;
        }

        _playerPosition = new Vector2(player.Position.X, player.Position.Y);
        _state = 2;
    }

    private bool CheckForObstacle(Vector2 position)
    {
        if (TileMapManager.IsCollidingWithTiles(position))
        {
            return true;
        }

        return false;
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
            _movingTime = 0.5f;
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
        var elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds / GameConstants.Constants.Config.SixtyFramesASecond);

        var tempX = Position.X;
        var tempY = Position.Y;

        Position.X += _direction.X * Speed * elapsedTime;

        if (TileMapManager.IsCollidingWithTiles(GetBox()) || DetectCollisionWithEnemies(enemies))
        {
            Position.X = tempX;
        }

        Position.Y += _direction.Y * Speed * elapsedTime;

        if (TileMapManager.IsCollidingWithTiles(GetBox()) || DetectCollisionWithEnemies(enemies))
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
        base.Draw(batch, gameTime);
    }
}
