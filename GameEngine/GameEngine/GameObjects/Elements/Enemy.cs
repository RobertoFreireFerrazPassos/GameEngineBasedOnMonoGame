using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using GameEngine.Enums;
using GameEngine.Elements.Managers;
using GameEngine.Elements;
using GameEngine.Utils;

namespace GameEngine.GameObjects.Elements;

public class Enemy : SpriteObject
{
    private int _state; // 0- far 1- wall  2-find
    private int Speed;
    private Vector2 _playerPosition;
    private List<Vector2> _positionsToPlayer = new List<Vector2>();
    private int _currentPositionToPlayerIndex = 0;
    private float _tolerancePositionToPlayer = 5f;
    private const float MovingTime = 1f;
    private float _movingTime = MovingTime;
    private float minDist = 40f;
    private float maxDist = 250f;

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
                    , new GameEngine.Elements.Texture("world",40, 26, 13, 40, 40)
                );
        AnimatedSprite.Ordering.Z = 2;
        CollisionBox = new CollisionBox(6, 17, 28, 18);
    }

    public void Update(GameTime gameTime, Player player, List<Enemy> enemies)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_state == 0 || _state == 1)
        {
            FindPlayer(player);
        }
        else if (_state == 2)
        {
            Move(elapsedTime, enemies);
        }
    }

    private void FindPlayer(Player player)
    {
        var playerCenter = player.GetBox().Center.ToVector2();
        var enemyCenter = GetBox().Center.ToVector2();
        var distance = Vector2.Distance(playerCenter, enemyCenter);

        if (distance < minDist)
        {
            return;
        }

        if (distance > maxDist)
        {
            _state = 0;
            return;
        }

        _currentPositionToPlayerIndex = 0;
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
        _positionsToPlayer = PositionUtils.GetPointsAlongLine(start, end, stepSize);
        foreach (var point in _positionsToPlayer)
        {
            if (obstacleCheck(point))
            {
                return true; 
            }
        }

        return false;
    }

    private void Move(float elapsedTime, List<Enemy> enemies)
    {
        _movingTime -= elapsedTime;

        if (_movingTime <= 0)
        {
            _movingTime = MovingTime;
            _state = 0;
        }

        UpdatePosition(elapsedTime, enemies);
    }

    private void UpdatePosition(float elapsedTime, List<Enemy> enemies)
    {
        var targetPosition = _positionsToPlayer[_currentPositionToPlayerIndex];
        var direction = targetPosition - GetBox().Center.ToVector2();
        var distance = direction.Length();

        if (distance < _tolerancePositionToPlayer)
        {
            _currentPositionToPlayerIndex = (_currentPositionToPlayerIndex + 1) % _positionsToPlayer.Count;
            return;
        }

        direction = direction * Speed * elapsedTime;
        direction.Normalize();

        var tempX = Position.X;
        var tempY = Position.Y;

        Position.X += direction.X;

        if (TileMapManager.IsCollidingWith(GetBox()) || DetectCollisionWithEnemies(enemies))
        {
            Position.X = tempX;
        }

        Position.Y += direction.Y;

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
        foreach (var position in _positionsToPlayer)
        {
            SpriteManager.DrawPixel(
                new Vector2(
                    position.X + (int)offset.X,
                    position.Y + (int)offset.Y
                ),Color.OrangeRed
            );
        }
    }
}
