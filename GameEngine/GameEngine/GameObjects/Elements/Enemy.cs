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
    private int _previousState;
    private int _currentState; // 0- far 1- wall  2-find

    public int State 
    {
        get {  
            return _currentState; 
        }
        set 
        {
            _previousState = _currentState;
            _currentState = value;
        }
    }

    private int Speed;
    private Vector2 _playerPosition;
    private List<Vector2> _positionsToPlayer = new List<Vector2>();
    private List<Vector2> _lastPositionsToPlayer = new List<Vector2>();
    private Timer _movingTimer = new Timer(1f);
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
        _movingTimer.OnTimerElapsed += OnMovementTimerElapsed;
    }

    public void Update(GameTime gameTime, Player player, List<Enemy> enemies)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (State == 0 || State == 1)
        {
            FindPlayer(player);
        }
        else if (State == 2)
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
            State = 0;
            return;
        }

        if (IsObstacleBetween(enemyCenter, playerCenter, 4, CheckForObstacle))
        {
            if (_previousState == 2)
            {
                _positionsToPlayer = _lastPositionsToPlayer;
                State = 2;
                return;
            }
            State = 1;
            return;
        }

        _playerPosition = playerCenter;
        State = 2;
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
        _movingTimer.StartIfInactive();
        _movingTimer.Update(elapsedTime);
        UpdatePosition(elapsedTime, enemies);
    }

    private void OnMovementTimerElapsed()
    {
        _lastPositionsToPlayer = _positionsToPlayer;
        State = 0;
    }

    private void UpdatePosition(float elapsedTime, List<Enemy> enemies)
    {
        var targetPosition = _positionsToPlayer[_positionsToPlayer.Count-1];
        var direction = targetPosition - GetBox().Center.ToVector2();
        var distance = direction.Length();

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
        if (State == 2)
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
